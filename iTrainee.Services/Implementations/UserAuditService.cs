using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace iTrainee.Services.Implementations
{
    public class UserAuditService : IUserAuditService
    {
        private IUserAuditRepository _userAuditRepository;

        public UserAuditService()
        {
        }

        public UserAuditService(IUserAuditRepository userAuditRepository)
        {
            _userAuditRepository = userAuditRepository;
        }

        public UserAudit GetUserAudit(int id)
        {
            return _userAuditRepository.GetUserAudit(id);
        }

        public int InsertUserAudit(UserAudit userAudit)
        {
            return _userAuditRepository.InsertUserAudit(userAudit);
        }
    }
}
