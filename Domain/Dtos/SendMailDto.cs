using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class SendMailDto
    {
        public IList<string> Emails { get; set; } = new List<string>();
        public IList<Attachment> Attachments { get; set; } = new List<Attachment>();
        public string Body { get; set; }
        public string Subject { get; set; }

    }
}
