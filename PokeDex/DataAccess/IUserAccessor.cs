using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IUserAccessor
    {
        int VerifyUserNameAndPassword(string email, string passwordHash);

        User SelectUserByEmail(string email);

        int UpdatePasswordHash(string email, string newPasswordHash, string oldPasswordHash);
    }
}
