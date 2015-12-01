using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTC.Models.Common
{
    public class NotificationModel
    {
        public int Id;

        public String Title;

        public String Message;

        public byte[] Avatar;

        public DateTime CreatedOn;
    }
}
