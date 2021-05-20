using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Logic
{
    public class MemberManager : IMemberManager
    {
        private IMemberAccesser _memberAccessor;

        public MemberManager() 
        {
            _memberAccessor = new MemberAccesser();
        }
        public MemberManager(IMemberAccesser memberAccessor) 
        {
            _memberAccessor = memberAccessor;
        }

        public bool AddNewMember(Member newMember)
        {
            bool result = false;
            int newMemberID = 0;
            try
            {
                newMemberID = _memberAccessor.InsertNewMember(newMember);
                if (newMemberID == 0) 
                {
                    throw new ApplicationException("New member was not added.");
                }
                result = true;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Add Member Failed.", ex);
            }

            return result;
        }

        public bool EditMemberProfile(Member oldMember, Member newMember, string newRole,
            string oldRole)
        {
            bool result = false;

            try
            {
                result = (1 == _memberAccessor.UpdateMemberProfile(oldMember, newMember));
                if (result == false) 
                {
                    throw new ApplicationException("Profile data not changed.");
                }
                _memberAccessor.UpdateMemberRole(oldMember.MemberID, oldRole, newRole);
                if (oldMember.Active != newMember.Active) 
                {
                    if (newMember.Active == true)
                    {
                        _memberAccessor.ReactivateMember(oldMember.MemberID);
                    }
                    else 
                    {
                        _memberAccessor.DeactivateMember(oldMember.MemberID);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update Failed.", ex);
            }

            return result;
        }

        public List<Member> RetrieveMemberByActive(bool active = true)
        {
            List<Member> members = null;

            try
            {
                members = _memberAccessor.SelectMemberByActive(active);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Member list not available.", ex);
            }

            return members;
        }

        public List<string> RetrieveAllRoles()
        {
            List<string> roles = null;

            try
            {
                roles = _memberAccessor.SelectAllRoles();
                if (roles == null) 
                {
                    roles = new List<string>();
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data Unavailable.", ex);
            }

            return roles;
        }
    }
}
