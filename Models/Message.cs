using System;
using System.Collections.Generic;

#nullable disable

namespace Messenger.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Un { get; set; }
        public string Msg { get; set; }
        public DateTime Sent { get; set; }

        public virtual User UnNavigation { get; set; }
    }
}