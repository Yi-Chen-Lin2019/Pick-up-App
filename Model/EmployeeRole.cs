using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class EmployeeRole
    {
        public EmployeeRole() {}

        public EmployeeRole(int EmployeeRoleId) {
            this.EmployeeRoleId = EmployeeRoleId;
        }
        public int EmployeeRoleId { get; set; }
    }
}
