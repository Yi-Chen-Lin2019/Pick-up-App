using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CustomerRole
    {
        public CustomerRole() {}

        public CustomerRole(int CustomerRoleId) {
            this.CustomerRoleId = CustomerRoleId;
        }
        public int CustomerRoleId { get; set; }
    }
}
