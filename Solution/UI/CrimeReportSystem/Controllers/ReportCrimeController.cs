using CrimeReportSystem.BL.Context;
using CrimeReportSystem.BL.Provider.ReportCrimeData;
using CrimeReportSystem.Models;
using CrimeReportSystem.Models.ReportCrime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO.Ports;
using System.Media;
using System.Windows.Input;
using SpeechLib;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using CrimeReportSystem.BL.Entities.ReportData;



namespace CrimeReportSystem.Controllers
{


    public class ReportCrimeController : TCRControllerBase
    {

        #region

        public IReportCrimeProvider ReportCrimeProvider { get; set; }
        public DataContext DataContext { get; set; }
        private bool _MustDisposeContext = true;

        public ReportCrimeController()
        {
            DataContext = new DataContext();
            ReportCrimeProvider = new ReportCrimeProvider(DataContext, CurrentUser);
        }
        public ReportCrimeController(IReportCrimeProvider reportCrimeProvider)
        {
            _MustDisposeContext = false;
            ReportCrimeProvider = reportCrimeProvider;
        }
        protected override void Dispose(bool disposing)
        {
            if (_MustDisposeContext)
                DataContext.Dispose();
            base.Dispose(disposing);
        }


        #endregion

        SpeechSynthesizer voice;
        //Prompt check;

        //public  void  test(string name ,string name2)
        //{
        //    voice  = new SpeechSynthesizer();
            
        //    voice.SelectVoiceByHints(VoiceGender.Female);
           
        //     voice.SpeakAsync("Message has been successfully tested" + name + name2);
                
              
           
          
        //}
      
        

        
                  
                 
                  
            
     

        
        // GET: ReportCrime
        [HttpPost]
        public ActionResult SaveReport(ReportCrimeModel model)
        {
            try
            {
                //returns a condition based on the request.
                if (!ModelState.IsValid)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error details");
    
                var report = ReportCrimeProvider.SaveReport(model.Id, model.TypeOfCrime, model.Location, model.Date, model.Time,model.Name,model.ContactNo,model.Status);
               
                model.Id = report.Id;
                return SerializeToAngular(model);
            }
            catch (ReportCrimeException e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        public ActionResult CrimeList()
        {
            var reports = ReportCrimeProvider.getCrimes().Where(a => a.Id == a.Id)
                            .Select(b => new KeyValueModel { Id = b.Id, Description = b.TypeOfCrime }).OrderBy(b => b.Description).ToList();

            return SerializeToAngular(reports);
        }
        public ActionResult ReportGrid(GridModel model)
        {

            int begin = SetupGridParams(model);

            var filteredQuery = ReportCrimeProvider.getCrimes()
                                                        .Select(a => new ReportGridModel()
                                                        {
                                                            Id = a.Id,
                                                            TypeOfCrime = a.TypeOfCrime,
                                                            Location = a.Location,
                                                            Time = a.Time,
                                                            Date =a.Date,
                                                            Name = a.Name,
                                                            Status = a.Status,
                                                            ContactNo =a.ContactNo

                                                        });
            if (model.Searchfor != "null")
            {
                filteredQuery = filteredQuery.Where(r => r.TypeOfCrime.Contains(model.Searchfor)
                                                    || r.Location.Contains(model.Searchfor)
                                                    || r.Date.Contains(model.Searchfor)
                                                    || r.Time.Contains(model.Searchfor)
                                                    || r.Name.Contains(model.Searchfor)
                                                    || r.Status.Contains(model.Searchfor)
                                                    );
            }
            //Get Reord count
            var totalNumberOfRecords = filteredQuery.Count();

            if (String.IsNullOrWhiteSpace(model.SortKey))
                filteredQuery = filteredQuery.OrderBy(a => a.TypeOfCrime); //default sort order

            //Setup sort order
            switch (model.SortOrder)
            {
                case "ASC":
                    switch (model.SortKey)
                    {
                        case "status":
                            filteredQuery = filteredQuery.OrderBy(r => r.Status);
                            break;
                        case "name":
                            filteredQuery = filteredQuery.OrderBy(r => r.Name);
                            break;
                        case "contactNo":
                            filteredQuery = filteredQuery.OrderBy(r => r.ContactNo);
                            break;
                        case "typeOfCrime":
                            filteredQuery = filteredQuery.OrderBy(r => r.TypeOfCrime);
                            break;
                        case "location":
                            filteredQuery = filteredQuery.OrderBy(r => r.Location);
                            break;
                        case "date":
                            filteredQuery = filteredQuery.OrderBy(r => r.Date);
                            break;
                        case "time":
                            filteredQuery = filteredQuery.OrderBy(r => r.Time);
                            break;

                    }
                    break;
                case "DESC":
                    switch (model.SortKey)
                    {
                        case "status":
                            filteredQuery = filteredQuery.OrderBy(r => r.Status);
                            break;
                        case "name":
                            filteredQuery = filteredQuery.OrderBy(r => r.Name);
                            break;
                        case "contactNo":
                            filteredQuery = filteredQuery.OrderBy(r => r.ContactNo);
                            break;
                        case "typeOfCrime":
                            filteredQuery = filteredQuery.OrderBy(r => r.TypeOfCrime);
                            break;
                        case "location":
                            filteredQuery = filteredQuery.OrderBy(r => r.Location);
                            break;
                        case "date":
                            filteredQuery = filteredQuery.OrderBy(r => r.Date);
                            break;
                        case "time":
                            filteredQuery = filteredQuery.OrderBy(r => r.Time);
                            break;
                    }
                    break;
            }

            //setup paging
            filteredQuery = filteredQuery.Skip(begin).Take(model.RecordsPerPage.Value);


            return SerializeToAngular(new GridResultModel<ReportGridModel>(filteredQuery.ToList(), totalNumberOfRecords));
        }

        public ActionResult GetReportCrimes(long? id)
        {
            if (id == null)
                id = 0;



            try
            {
                
                  SerialPort _serialPort = new SerialPort();
             

          
                 _serialPort.PortName = "COM4";
                 _serialPort.BaudRate = 9600;
                 _serialPort.Open();
                 
               
               /*if(!_serialPort.IsOpen)
               { _serialPort.Open(); }*/



                 ReportCrime rep = new ReportCrime();
                
                
                //do some work here on the provider 
                var model = ReportCrimeProvider.getCrimes().Where(a => a.Id == id).Select(a => new ReportCrimeModel()
                {
                    Id = a.Id,
                    Status = a.Status,
                    Name = a.Name,
                    ContactNo = a.ContactNo,
                   TypeOfCrime = a.TypeOfCrime,
                   Location = a.Location,
                   Date = a.Date,
                   Time = a.Time
                   

                   

                }).Single();
               // _serialPort.Write("1"); 
                //dont forget to substring the location to 32 bit characters
                string dataList = model.Location+"-"+model.TypeOfCrime;
                string dataset = "";
                if(dataList.Length > 27)
                {
                   dataset =  dataList.Substring(0,26);
                }
                else
                {
                    dataset = dataList;
                }

                _serialPort.Write(dataset+".");
                _serialPort.Close();
                rep = DataContext.ReportCrimeSet.Where(a => a.Id == model.Id).SingleOrDefault();
                rep.Status = "Reported";
                DataContext.SaveChanges();
               
               //test(model.TypeOfCrime, model.Location);
                
                return SerializeToAngular(model);
            }
            catch (ReportCrimeException e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, e.Message);
            }

          
        }
    }
}