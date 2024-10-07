using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibruaryManagementSystem.DAL
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int MemberId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }

}
