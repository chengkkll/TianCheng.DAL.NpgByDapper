using BenchmarkDotNet.Attributes;
using NpgBenchmark.DAL;
using NpgBenchmark.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TianCheng.DAL.NpgByDapper;

namespace NpgBenchmark.Benchmark
{
    [SimpleJob(targetCount: 1000)]
    public class SearchConditionMain
    {
        static public MockGuidDAL GuidDal = new MockGuidDAL();
        private static readonly Random rd = new Random();
        private static readonly string str = "abcdefghijklmnopqrstuvwxyz";
        static public string RandomName()
        {
            return $"{str.Substring(rd.Next(0, 26), 1)}{str.Substring(rd.Next(0, 26), 1)}";
        }

        public SearchConditionMain()
        {
            ReadJson.Init();
            // 设置AutoMapper映射信息
            AutoMapperExtension.RegisterAutoMapper();
        }

        [Benchmark]
        public void 条件查询Dapper_SQL()
        {
            DapperIntDal.SearchName(RandomName());
        }

        [Benchmark]
        public void 条件查询封装()
        {
            GuidDal.Search(new GuidQuery() { LikeName = RandomName() });
        }

        [Benchmark]
        public void 条件查询转对象()
        {
            GuidDal.Search<MockGuidInfo>(new GuidQuery() { LikeName = RandomName() });
        }

        [Benchmark]
        public MockGuidInfo 条件查询First()
        {
            return GuidDal.First(new GuidQuery() { LikeName = RandomName() });
        }

        [Benchmark]
        public int 条件查询数量()
        {
            return GuidDal.Count(new GuidQuery() { LikeName = RandomName() });
        }
    }
}
