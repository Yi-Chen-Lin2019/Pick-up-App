using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class RoleManagement
    {
        public bool InsertDefaultRoles(List<Role> roles)
        {
            IPersonRepository pRepo = new PersonRepository();
            int result = 0;
            foreach (var item in roles)
            {
                if(pRepo.InsertRole(item) != null) { result++; };
            }
            return (roles.Count == result);
        }
    }
}
