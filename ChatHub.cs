using System;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Messenger.Models;

namespace Messenger
{
    public class ChatHub : Hub
    {
        public async Task Register(string unFromClient, string pwFromClient)
        {
            string result;

            using (MessengerContext db = new MessengerContext())
            {
                if (db.Users.SingleOrDefault(u => u.Un == unFromClient) == null)
                {
                    User userToServer = new User { Un = unFromClient, Pw = pwFromClient };
                    db.Users.Add(userToServer);
                    db.SaveChanges();

                    if (db.Users.SingleOrDefault(u => u.Un == unFromClient) == null) result = "Ошибка!";
                    else result = "Регистрация прошла успешно";
                }
                else result = "Пользователь с таким именем уже существует!";
            }

            await this.Clients.Caller.SendAsync("RegResult", result);
        }

        public async Task Check(string unFromClient, string pwFromClient)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(Startup.Configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT COUNT(*)::int FROM users WHERE un = @p1 AND pw = crypt(@p2, pw)", conn))
                {
                    cmd.Parameters.AddWithValue("@p1", unFromClient);
                    cmd.Parameters.AddWithValue("@p2", pwFromClient);
                    bool result = Convert.ToInt32(cmd.ExecuteScalar()) == 0? false : true;

                    await this.Clients.Caller.SendAsync("Check", result);
                }
            }
        }

        public async Task Send(string unFromClient, string msgFromClient)
        {
            DateTime now = DateTime.Now;

            await this.Clients.All.SendAsync("Send", unFromClient, msgFromClient);

            await Task.Run(() =>
            {
                using (MessengerContext db = new MessengerContext())
                {
                    Message msgToServer = new Message { Un = unFromClient, Msg = msgFromClient, Sent = now };
                    db.Messages.Add(msgToServer);
                    db.SaveChanges();
                }
            });
        }
    }
}