using Microsoft.Extensions.Configuration;
using NpgBenchmark.Model;
using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.DAL.NpgByDapper;

namespace NpgBenchmark
{
    public class ReadJson
    {
        static public void Init()
        {
            string path = Environment.CurrentDirectory.Split("Release").Length > 2 ?
                            Environment.CurrentDirectory + "\\..\\..\\..\\..\\..\\..\\..\\" :
                            Environment.CurrentDirectory + "\\..\\..\\..\\";
            System.IO.File.Copy($"{path}\\db.config.json", $"{Environment.CurrentDirectory}\\db.config.json", true);
        }

        static public List<MockGuidBase> Load()
        {
            string path = Environment.CurrentDirectory.Split("Release").Length > 2 ?
                            Environment.CurrentDirectory + "\\..\\..\\..\\..\\..\\..\\..\\" :
                            Environment.CurrentDirectory + "\\..\\..\\..\\";
            
            // 想要更多的测试数据，就在https://www.mockaroo.com/下吧
            System.IO.File.Copy($"{path}\\db.config.json", $"{Environment.CurrentDirectory}\\db.config.json", true);
            string text1 = System.IO.File.ReadAllText($"{path}\\data\\MOCK_DATA1.json");
            string text2 = System.IO.File.ReadAllText($"{path}\\data\\MOCK_DATA2.json");
            string text3 = System.IO.File.ReadAllText($"{path}\\data\\MOCK_DATA3.json");
            string text4 = System.IO.File.ReadAllText($"{path}\\data\\MOCK_DATA4.json");
            string text5 = System.IO.File.ReadAllText($"{path}\\data\\MOCK_DATA5.json");
            string text6 = System.IO.File.ReadAllText($"{path}\\data\\MOCK_DATA5.json");
            string text7 = System.IO.File.ReadAllText($"{path}\\data\\MOCK_DATA5.json");
            string text8 = System.IO.File.ReadAllText($"{path}\\data\\MOCK_DATA5.json");
            string text9 = System.IO.File.ReadAllText($"{path}\\data\\MOCK_DATA5.json");
            var result = text1.JsonToObject<List<MockGuidBase>>();
            result.AddRange(text2.JsonToObject<List<MockGuidBase>>());
            result.AddRange(text3.JsonToObject<List<MockGuidBase>>());
            result.AddRange(text4.JsonToObject<List<MockGuidBase>>());
            result.AddRange(text5.JsonToObject<List<MockGuidBase>>());
            result.AddRange(text6.JsonToObject<List<MockGuidBase>>());
            result.AddRange(text7.JsonToObject<List<MockGuidBase>>());
            result.AddRange(text8.JsonToObject<List<MockGuidBase>>());
            result.AddRange(text9.JsonToObject<List<MockGuidBase>>());
            return result;
        }
    }
}
