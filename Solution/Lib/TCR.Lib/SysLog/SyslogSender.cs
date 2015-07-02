using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Net;
using System.Configuration;

namespace TCR.Lib.SysLog
{
   

    public class SyslogSender
    {
        private const Facility FACILITY = Facility.Local0;

        private const string NILVALUE = "-";

        private static string _HostName = null;
        private static string HostName
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_HostName))
                    _HostName = Dns.GetHostEntry(Environment.MachineName).HostName;
                return _HostName;
            }
        }


        private static string _ProcessId = null;
        private static string ProcessId
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_ProcessId))
                {
                    var process = Process.GetCurrentProcess();
                    _ProcessId = process.Id.ToString();
                    _ProcessName = process.ProcessName;
                }
                return _ProcessId;
            }
        }

        private static string _ProcessName = null;
        private static string ProcessName
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_ProcessName))
                {
                    var process = Process.GetCurrentProcess();
                    _ProcessId = process.Id.ToString();
                    _ProcessName = process.ProcessName;
                }
                return _ProcessName;
            }

        }


        /// <summary>
        /// the protocol version
        /// </summary>
        private const int VERSION = 1;

        private static byte[] ConstructMessage(string AppName, Level level, Facility facility, string messageID, string message = "", string ProcID = "")
        {

            if (!string.IsNullOrEmpty(message) && message.Length > 512)
            {

                message = message.Substring(0, 508) + "...";
            }

            int prival = ((int)facility) * 8 + ((int)level);
            string pri = string.Format("<{0}>", prival);
            string timestamp =
                new DateTimeOffset(DateTime.Now, TimeZoneInfo.Local.GetUtcOffset(DateTime.Now)).ToString("yyyy-MM-ddTHH:mm:ss.ffffffzzz");
            string appName = string.IsNullOrWhiteSpace(AppName) ? NILVALUE : AppName;
            string procId = string.IsNullOrWhiteSpace(ProcID) ? ProcessId : ProcID;
            string msgId = string.IsNullOrWhiteSpace(messageID) ? NILVALUE : messageID;

            string header = string.Format("{0}{1} {2} {3} {4} {5} {6}", pri, VERSION, timestamp, HostName, appName, procId, msgId);
            string SD = NILVALUE;

            List<byte> syslogMsg = new List<byte>();
            syslogMsg.AddRange(System.Text.Encoding.ASCII.GetBytes(header));
            syslogMsg.AddRange(System.Text.Encoding.ASCII.GetBytes(" "));
            syslogMsg.AddRange(System.Text.Encoding.ASCII.GetBytes(SD));

            if (!String.IsNullOrWhiteSpace(message))
                message = message.Replace("\n", "").Replace("\r", "");

            if (!String.IsNullOrWhiteSpace(message))
            {
                syslogMsg.AddRange(System.Text.Encoding.ASCII.GetBytes(" "));
                syslogMsg.AddRange(System.Text.Encoding.UTF8.GetBytes(message));
            }

            return syslogMsg.ToArray();
        }

        //http://www.syslog.org/logged/logging-and-syslog-best-practices/
        public static void SendMessage(Level priority, object originator, string theMessage)
        {
            string processID = "";
            if (originator is string)
                processID = originator as string;
            else
                processID = originator.GetType().ToString();
            string syslogServer = ConfigurationManager.AppSettings["SyslogServer"] as string;
            if (String.IsNullOrWhiteSpace(syslogServer))
                syslogServer = "127.0.0.1";

            using (UdpClient udp = new UdpClient(syslogServer, 514))
            {
                // Create a byte to hold our strParams (data) in           
                byte[] rawMsg = ConstructMessage(ProcessName, priority, FACILITY, "", theMessage, processID);
                udp.Client.SendBufferSize = 4096;
                udp.Send(rawMsg, rawMsg.Length);
                udp.Close();
            }
        }


        public static void SendInformation(object originator, string infoMessage)
        {
            SendMessage(Level.Informational, originator, infoMessage);
        }



        public static void SendError(object originator, string errorMessage)
        {
            SendMessage(Level.Error, originator, errorMessage);

        }

        public static void SendWarning(object originator, string infoMessage)
        {
            SendMessage(Level.Warning, originator, infoMessage);
        }

        public static void SendError(object originator, Exception e)
        {
            string errorMessage = e.Message;
            //if (e.StackTrace != null)
            //    errorMessage += " stack:" + e.StackTrace;
            SendMessage(Level.Error, originator, errorMessage);

            if (e.InnerException != null) //recurse down the inner exceptions
                SendError(originator, e.InnerException);
        }

        public static void SendCriticalError(object originator, Exception e)
        {
            string errorMessage = e.Message;
            //if (e.StackTrace != null)
            //    errorMessage += " stack:" + e.StackTrace;
            SendMessage(Level.Alert, originator, errorMessage);

            if (e.InnerException != null) //recurse down the inner exceptions
                SendError(originator, e.InnerException);
        }

        public static void SendCriticalError(object originator, string errorMessage)
        {
            SendMessage(Level.Alert, originator, errorMessage);
        }
    }
}
