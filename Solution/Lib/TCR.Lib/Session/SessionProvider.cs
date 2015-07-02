using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace TCR.Lib.Session
{
    /*
     * To Configure to run in SQL :
     * Web Config in <System.Web>:   <sessionState  mode="SQLServer" allowCustomSqlDatabase="true" sqlConnectionString="Data Source=<serverName>;Initial Catalog=<databaseName>;User ID=<userName>;Password=<password>" timeout="30"  cookieless="false"/>
     * Command Line :  aspnet_regsql.exe -S <servername> -U <username> -P <password> -ssadd -sstype c -d <databaseName>
     
     */
    public class SessionProvider : ISessionProvider
    {
        public string Add<T>(T serializableObject, bool storeApplicationWide = false)
        {
            string key = System.Guid.NewGuid().ToString();
            Add<T>(key, serializableObject, storeApplicationWide);

            return key;
        }

        public T GetObject<T>(string key, bool storeApplicationWide = false)
        {
            object value = null;
            if (storeApplicationWide)
                value = ApplicationSession[key];
            else
                value = LocalSession[key];
            return value == null ? default(T) : (T)value;
        }

        private HttpSessionState LocalSession
        {
            get
            {
                return System.Web.HttpContext.Current.Session;
            }
        }

        private HttpApplicationState ApplicationSession
        {
            get
            {
                return System.Web.HttpContext.Current.Application;
            }
        }

        public void Add<T>(string key, T serializableObject, bool storeApplicationWide = false)
        {
            if (storeApplicationWide)
                ApplicationSession[key] = serializableObject;
            else
                LocalSession[key] = serializableObject;
        }
    }
}
