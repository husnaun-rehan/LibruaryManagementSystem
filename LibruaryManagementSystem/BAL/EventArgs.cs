using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibruaryManagementSystem.DAL
{
    public class NotificationEventArgs : EventArgs
    {
        public string Message { get; set; }
    }

}
