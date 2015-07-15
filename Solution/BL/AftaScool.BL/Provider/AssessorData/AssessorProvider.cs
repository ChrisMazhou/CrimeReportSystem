using AftaScool.BL.Context;
using AftaScool.BL.Entities.AssessorData;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Provider.Security;
using AftaScool.BL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.AssessorData
{
    public class AssessorProvider : AftaScoolProvider, IAssessorProvider
    {
        #region Ctor

        public AssessorProvider(DataContext context, ICurrentUser currentUser)
            : base(context, currentUser)
        { }
        #endregion



        public Assessor createAssessor(long? id, long userId)
        {

            Authenticate(PrivilegeType.AssessorMaintenance);



            Assessor saveAssessor = new Assessor();


            // saveAssessor = DataContext.AssessorSet.Where(a => a.UserIdentityId == userId).SingleOrDefault();

            if (id != null && id > 0)
                saveAssessor = DataContext.AssessorSet.Where(a => a.Id == id).SingleOrDefault();
            else
            {
                saveAssessor = new Assessor();
                DataContext.AssessorSet.Add(saveAssessor);
            }

            saveAssessor.UserIdentityId = userId;
            DataContextSaveChanges();
            return saveAssessor;

        }

        public IQueryable<Assessor> GetAssessor()
        {

            var q = from h in DataContext.AssessorSet
                    orderby h.UserIdentityId
                    select h;

            return q;

        }




    }
}