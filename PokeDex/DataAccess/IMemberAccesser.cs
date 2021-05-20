using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IMemberAccesser
    {
        List<Member> SelectMemberByActive(bool active = true);
        List<string> SelectAllRoles();
        int UpdateMemberProfile(Member oldMember, Member newMember);
        int DeactivateMember(int memberID);
        int ReactivateMember(int memberID);
        int UpdateMemberRole(int memberID, string oldRole, string newRole);
        int InsertNewMember(Member member);
    }
}
