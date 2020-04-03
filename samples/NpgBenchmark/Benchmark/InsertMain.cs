using BenchmarkDotNet.Attributes;
using NpgBenchmark.DAL;
using NpgBenchmark.Model;
using System;
using System.Collections.Generic;
using System.Data;
using TianCheng.DAL.NpgByDapper;

namespace NpgBenchmark.Benchmark
{
    [SimpleJob(targetCount: 1000)]
    public class InsertMain
    {
        public MockGuidDAL GuidDal = new MockGuidDAL();
        public MockIntDal IntDal = new MockIntDal();

        public Stack<MockGuidBase> gdl = new Stack<MockGuidBase>();
        public Stack<MockGuidBase> idl = new Stack<MockGuidBase>();

        public InsertMain()
        {
            ReadJson.Init();
            // 设置AutoMapper映射信息
            AutoMapperExtension.RegisterAutoMapper();

            foreach (var item in ReadJson.Load())
            {
                gdl.Push(item);
                idl.Push(item);
            }
        }
       
        private MockGuidDB GetGuidDB()
        {
            var info = gdl.Pop().AutoMapper<MockGuidDB>();
            info.id = Guid.NewGuid();
            return info;
        }
        private MockIntDB GetIntDB()
        {
            return idl.Pop().AutoMapper<MockIntDB>();
        }

        [Benchmark]
        public void 单条guid主键插入Dapper_SQL()
        {
            DapperIntDal.InsertGuid(GetGuidDB());
        }

        [Benchmark]
        public void 单条guid主键插入_封装后()
        {
            GuidDal.Insert(GetGuidDB());
        }

        [Benchmark]
        public void 单条int主键插入Dapper_SQL()
        {
            DapperIntDal.InsertInt(GetIntDB());
        }

        [Benchmark]
        public void 单条int主键插入_封装后()
        {
            IntDal.Insert(GetIntDB());
        }

        [Benchmark]
        public void 事务插入三条数据()
        {
            IDbTransaction tran = GuidDal.ConnTran(out IDbConnection connection);
            try
            {
                GuidDal.Insert(GetGuidDB(), tran);
                GuidDal.Insert(GetGuidDB(), tran);
                GuidDal.Insert(GetGuidDB(), tran);
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
            }
            finally
            {
                GuidDal.CloseTran(connection, tran);
            }
        }

        [Benchmark]
        public void 三条数据单语句插入()
        {
            IList<MockGuidDB> list = new List<MockGuidDB>
            {
                GetGuidDB(),
                GetGuidDB(),
                GetGuidDB()
            };
            GuidDal.Insert(list);
        }
    }
}
