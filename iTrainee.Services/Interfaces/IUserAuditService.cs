using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Services.Interfaces
{
    public interface IUserAuditService
    {
        IEnumerable<UserAudit> GetAllUserAudit();

        UserAudit GetUserAudit(int id);

        int InsertUserAudit(UserAudit userAudit);
    }
}