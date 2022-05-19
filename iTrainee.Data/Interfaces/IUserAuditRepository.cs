using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Data.Interfaces
{
    public interface IUserAuditRepository
    {
        List<UserAudit> GetAllUserAudit();

        UserAudit GetUserAudit(int id);

        int InsertUserAudit(UserAudit userAudit);
    }
}