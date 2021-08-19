using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;

namespace IIS_AppPoolsReset {
    class Program {
        static void Main(string[] args) {
            KillAspNetProcess();
            StartAspNetProcess();
        }

        private static void KillAspNetProcess() {
            string processName = "aspnet_wp";
            System.OperatingSystem os = System.Environment.OSVersion;

            //Longhorn and Windows Server 2003 use w3wp.exe
            if ((os.Version.Major == 5 && os.Version.Minor > 1) || os.Version.Major == 6)
                processName = "w3wp";

            foreach (Process process in Process.GetProcessesByName(processName)) {
                Console.WriteLine("Killing ASP.NET worker process (Process ID:" + process.Id + ")");
                process.Kill();
            }
        }

        private static void StartAspNetProcess() {
            try {
                string phonyURL = "https://www.scmh.org.tw";
                WebClient myWebClient = new WebClient();
                byte[] myDataBuffer = myWebClient.DownloadData(phonyURL);
            } catch (System.Net.WebException) {
                Console.WriteLine("ASP.NET worker process restarted.");
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
