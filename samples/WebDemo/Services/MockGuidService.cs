using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.DAL;
using WebDemo.Model;

namespace WebDemo.Services
{
    public class MockGuidService
    {
        MockGuidDAL Dal;
        public MockGuidService(MockGuidDAL dal)
        {
            Dal = dal;
        }

        public MockGuidInfo First()
        {
            return Dal.First(new MockGuidQuery() { LikeName = "c" });
        }
    }
}
