using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCR.Lib.Session;
using AftaScool.BL.Provider.Security;
using AftaScool.Models;
using AftaScool.Models.Account;

namespace AftaScool.Controllers
{
    public class TCRControllerBase : Controller
    {
        protected ISessionProvider SessionProvider { get; set; }

        public TCRControllerBase()
            :this(new SessionProvider())
        {

        }
        public TCRControllerBase(ISessionProvider sessionProvider)
        {
            SessionProvider = sessionProvider;
        }

        protected ContentResult SerializeToAngular(object o)
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(o, camelCaseFormatter),
                ContentType = "application/json"
            };
            return jsonResult;
        }

        protected CurrentUserModel CurrentUser
        {
            get
            {
                return SessionProvider.GetObject<CurrentUserModel>("CurrentUser");
            }
            set
            {
                SessionProvider.Add<CurrentUserModel>("CurrentUser", value);
            }
        }

        protected int SetupGridParams(GridModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Searchfor))
                model.Searchfor = "null";
            if (string.IsNullOrWhiteSpace(model.SortKey))
                model.SortKey = string.Empty;
            if (string.IsNullOrWhiteSpace(model.SortOrder))
                model.SortOrder = "ASC";

            model.SortKey = model.SortKey.ToLower();

            if (model.CurrentPage == null)
                model.CurrentPage = 1;
            if (model.RecordsPerPage == null)
                model.RecordsPerPage = 100;

            int begin = (model.CurrentPage.Value - 1) * model.RecordsPerPage.Value;
            return begin;
        }

    }
}
