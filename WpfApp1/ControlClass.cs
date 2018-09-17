using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    enum role
    {
        sysadmin,
        serveradmin,
        securityadmin,
        processadmin,
        setupadmin,
        bulkadmin,
        discadmin,
        dbcreator
    }
    class ControlClass:Node
    {
        public role role { set; get; }   
        public Int32 value { set; get; }
        public String FirstName { get; set; }
        
    }
}
