using System;
using System.Collections.Generic;

#nullable disable

namespace Messenger.Models
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
        }

        public string Un { get; set; }
        public string Pw { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}