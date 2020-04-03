using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// 程序集操作
    /// </summary>
    public class AssemblyProvider
    {
        /// <summary>
        /// 获取当前目录下有效的程序集
        /// </summary>
        /// <returns></returns>
        static private List<Assembly> GetAssemblyList()
        {
            Dictionary<string, Assembly> assemblyDict = new Dictionary<string, Assembly>();
            // 获取项目通过Nuget引用的程序集
            foreach (var library in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.FullName));
                    if (ConnectionProvider.Options.Assembly.Any(a => assembly.FullName.Contains(a)))
                    {
                        if (assembly != null && assembly.GetName() != null && !assemblyDict.ContainsKey(assembly.GetName().Name))
                            assemblyDict.Add(assembly.GetName().Name, assembly);
                    }
                }
                catch
                {
                    // 程序集无法反射时跳过
                }
            }

            // 获取项目所在目录的程序集
            foreach (var file in System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
            {
                try
                {
                    var assembly = Assembly.LoadFrom(file);
                    if (ConnectionProvider.Options.Assembly.Any(a => assembly.FullName.Contains(a)))
                    {
                        if (assembly != null && assembly.GetName() != null && !assemblyDict.ContainsKey(assembly.GetName().Name))
                            assemblyDict.Add(assembly.GetName().Name, assembly);
                    }
                }
                catch
                {
                    // 程序集无法反射时跳过
                }
            }
            // 返回有效的程序集
            return assemblyDict.Values.ToList();
        }

        /// <summary>
        /// 根据接口获取对象类型
        /// </summary>
        /// <returns></returns>
        static public IEnumerable<Type> GetTypeByInterface<I>()
        {
            string interfaceName = typeof(I).Name;

            IList<Type> result = new List<Type>();
            foreach (var assembly in GetAssemblyList())
            {
                foreach (var type in assembly.GetTypes())
                {
                    try
                    {
                        if (type.GetInterfaces().Where(i => i.ToString().Contains(interfaceName)).Count() > 0)  // i.FullName 会有为空的情况
                        {
                            result.Add(type);
                        }
                    }
                    catch (Exception ex)
                    {
                        TianCheng.Log.CommonLog.Logger.Error(ex, $"根据接口名称获取对象类型时出错。程序集：{assembly.FullName}\r\n类型:{type.Name}");
                    }
                }
            }
            return result;
        }
    }
}
