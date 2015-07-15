using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AftaScool.BL.Entities.SchoolData;


namespace AftaScool.BL.Provider.SchoolData
{
    public interface ISchoolProvider:IAftaScoolProvider
    {
        
        School SchoolSave(long? id, string schoolName, String addressline1, string addressline2, string city, string postalCode);
        
        void ArchiveSchool(long id);

        IQueryable<School> GetSchools();

         School GetSchool(long id);
       
    }
}
