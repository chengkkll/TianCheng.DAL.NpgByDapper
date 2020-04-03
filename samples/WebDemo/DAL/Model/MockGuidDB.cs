using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TianCheng.DAL.NpgByDapper;

namespace WebDemo.DAL
{
    public class MockGuidDB : GuidId
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }

        public string gender { get; set; }
        public string ip_address { get; set; }
    }
}
