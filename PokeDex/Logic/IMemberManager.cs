using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IMemberManager
    {
        List<Member> RetrieveMemberByActive(bool active = true);
        List<string> RetrieveAllRoles();
        bool EditMemberProfile(Member oldMember, Member newMember, string newRole, 
            string oldRole);
        bool AddNewMember(Member newMember);
    }
}
