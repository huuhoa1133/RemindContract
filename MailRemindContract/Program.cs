using MailRemindContract.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailRemindContract
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectDb = new ConnectDB();
            var _db = connectDb.GetDB();
            var schedule = new ScheduleRepository(_db);
            var task = schedule.RemindContract();
            Task.WaitAll(task);
            Console.WriteLine("done");
        }



    }

    public class dev
    {
        public void test()
        {
            var to = "hoahuynh@landsoft.com;hoahuynh@landsoft.com";
            var tos = to.Split(';');
            var x = FluentMail.CreateEmailFrom("landsofthcm@gmail.com")
                    .BCC("abc@gm.com")
                    .Subject("hoa huynh")
                    .To(to.Split(';'))
                    .Body("body")
                    .SendAsync();
            Task.WaitAll(x);
        }
    }

}
