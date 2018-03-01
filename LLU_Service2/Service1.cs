using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LLU_Service2
{
    public partial class Scheduler : ServiceBase
    {
        private Timer timer1 = null;
        public Scheduler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer();
            this.timer1.Interval = 3000;
            //this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_SQLValidate);
            Library.writeErrorLog("LLU_Servic2 started");
        }
        private void timer1_SQLValidate(object sender, ElapsedEventArgs e)
        {
            Library.ValidateSQLContent();
        }

/*        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            Library.writeErrorLog("Timer has done a succesfull job");
        }
*/
        protected override void OnStop()
        {
            Library.writeErrorLog("LLU_Servic2 stopped");
        }
    }
}
