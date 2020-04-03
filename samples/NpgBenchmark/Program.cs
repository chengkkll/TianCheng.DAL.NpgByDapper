using BenchmarkDotNet.Running;
using NpgBenchmark.Benchmark;
using NpgBenchmark.DAL;
using System;
using System.Threading.Tasks;
using TianCheng.DAL.NpgByDapper;

namespace NpgBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadJson.Init();

#if RELEASE
            BenchmarkRunner.Run<InsertMain>();
#endif

#if DEBUG
            var dal = new SearchConditionMain();
            MockGuidDAL GuidDal = new MockGuidDAL();
            MockIntDal IntDal = new MockIntDal();
            GuidDal.Drop();
            IntDal.Drop();

            for (int i = 0; i < 1; i++)
            {
                //Task.Run(() =>
                //{
                //var result =
                dal.条件查询Dapper_SQL();
                dal.条件查询封装();
                dal.条件查询转对象();
                var result = dal.条件查询数量();
                //dal.分页查询();
                Console.WriteLine($"item : {i}");
                Console.WriteLine($"result : {result.ToJson()}");
                //});
            }
#endif
            Console.WriteLine("操作完成");
            Console.ReadLine();
        }
    }
}
