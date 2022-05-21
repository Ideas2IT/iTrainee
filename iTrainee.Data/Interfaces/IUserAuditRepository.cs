using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Data.Interfaces
{
    public interface IUserAuditRepository
    {
        UserAudit GetUserAudit(int id);

        int InsertUserAudit(UserAudit userAudit);
    }
}