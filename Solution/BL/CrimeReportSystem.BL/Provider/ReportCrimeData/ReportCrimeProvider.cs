using CrimeReportSystem.BL.Context;
using CrimeReportSystem.BL.Entities.ReportData;
using CrimeReportSystem.BL.Entities.SecurityData;
using CrimeReportSystem.BL.Provider.ReportCrimeData;
using CrimeReportSystem.BL.Provider.Security;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;




namespace CrimeReportSystem.BL.Provider.ReportCrimeData
{
    public class ReportCrimeProvider:CrimeReportSystemProvider,IReportCrimeProvider
    {


       #region Ctor

        public ReportCrimeProvider(DataContext context, ICurrentUser currentUser)
            : base(context, currentUser)
        { }
     

        #endregion


       public ReportCrime SaveReport(long? id,string typeOfCrime,string location, string date,string time,string name,string contactNo,string status)
        {


          /*  Authenticate(PrivilegeType.ReportCrimeMaintenance);*/
            ReportCrime rep = new ReportCrime();
            DataTable dt;

         
          //  Outlook.Folder deletee = new Outlook.Folder();

         //  Outlook.MailItem info = new Outlook.MailItem();
            Outlook._Application _app = new Outlook.Application();
            Outlook._NameSpace _ns = _app.GetNamespace("MAPI");
            Outlook.MAPIFolder inbox = _ns.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
           // Outlook.MAPIFolder delete = _ns.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderDeletedItems);
            _ns.SendAndReceive(true);

         

            dt = new DataTable("Inbox");
            dt.Columns.Add("Subject", typeof(string));
            dt.Columns.Add("Sender", typeof(string));
            dt.Columns.Add("Body", typeof(string));
            dt.Columns.Add("Date", typeof(string));

           string subItem ="";
         
            

            foreach (Outlook.MailItem item in inbox.Items)
            {


                if (item.UnRead == true)
                {
                     subItem = item.Subject;
                     int testChar = subItem.IndexOf("-");
                    
                    if(subItem.IndexOf("-") > 0)
                    {
                        for (int x = 0; x < subItem.Length; x++)
                        {

                            if (subItem.Substring(x, 1) == "-" && testChar == x)
                            {
                                typeOfCrime = subItem.Substring(0, x);
                            }

                            if (subItem.Substring(x, 1) == "-" && testChar != x)
                            {
                                name = subItem.Substring(typeOfCrime.Length + 1, x - typeOfCrime.Length - 1 );

                                contactNo = subItem.Substring(x + 1);
                            }

                        }
                    }
                    else
                    {
                        typeOfCrime = item.Subject;
                        name = "Anonymous";
                        contactNo = "Anonymous";
                    }


                    status = "Pending";

                   
                    location = item.Body;

                    date = item.SentOn.ToLongDateString();
                    time = item.SentOn.ToLongTimeString();

                    rep.Status = status;
                    rep.TypeOfCrime = typeOfCrime;
                    rep.Name = name;
                    rep.ContactNo = contactNo;
                    rep.Location = location;
                    rep.Date = date;
                    rep.Time = time;
                  
                    

                    DataContext.ReportCrimeSet.Add(rep);
                    DataContext.SaveChanges();

                   
                    
                    
                }

                if (item.UnRead)
                {
                    item.UnRead = false;
                    item.Save();
                }

            
               
                   

              


            }


          //  _serialPort.Write("1");

         
           
            //_serialPort.Close();
                    

          
    

                //Outlook.ContactItem contact =

                // _ns.geGetDefaultFolder(Outlook.OlDefaultFolders.olFolderContacts).
                // Items.
                // Find(
                // string.Format("[LastName]='{0}' AND [FirstName]='{1}'",
                // lastName, firstName))
                // as Outlook.ContactItem;

                //if (contact != null)
                //{
                //    contact.Delete();
                //}









                return rep;



        }
        public IQueryable<ReportCrime> getCrimes()
        {
            var q = from h in DataContext.ReportCrimeSet
                    orderby h.TypeOfCrime
                    select h;

            return q;
        }

        public ReportCrime ArchiveReport(long id)
        {
            Authenticate(PrivilegeType.ReportCrimeMaintenance);
            var info = DataContext.ReportCrimeSet.Where(a => a.Id == id).SingleOrDefault();
            DataContextSaveChanges();


            

            return info;

        }




    }
}