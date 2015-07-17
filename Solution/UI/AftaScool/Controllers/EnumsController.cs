using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TCR.Lib.Utility;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Types;
using AftaScool.Models.Enum;


namespace AftaScool.Controllers
{
    [AllowAnonymous]
    public class EnumsController : TCRControllerBase
    {

        public ActionResult StatusTypeEnum()
        {
            return EnumToModel<StatusType>();
        }

        public ActionResult GenderTypeEnum()
        {
            return EnumToModel<GenderType>();
        }

        public ActionResult PrivilegeTypeEnum()
        {
            return EnumToModel<PrivilegeType>();
        }

        private ContentResult EnumToModel<T>(bool sort = true)
        {
            List<EnumModel> e = new List<EnumModel>();

            foreach (T eValue in Enum.GetValues(typeof(T)))
            {
                e.Add(new EnumModel() { Description = NameSplitting.SplitCamelCase(eValue), Value = eValue.ToString() });
            }

            if (sort)
            {
                e = e.OrderBy(a => a.Description).ToList();
            }
            return SerializeToAngular(e);
        }
    }
}
