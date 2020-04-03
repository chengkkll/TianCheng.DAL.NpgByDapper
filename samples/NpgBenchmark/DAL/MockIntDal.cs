using NpgBenchmark.Model;
using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.DAL.NpgByDapper;

namespace NpgBenchmark.DAL
{
    public class MockIntDal : IntOperation<MockIntDB>
    {
        protected override string TableName { get; set; } = "mock_serial";
    }
}
