using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Services.Interfaces
{
    public interface IUserAuditService
    {
        UserAudit GetUserAudit(int id);

        int InsertUserAudit(UserAudit userAudit);
    }
}