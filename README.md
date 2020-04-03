# TianCheng.DAL.NpgByDapper

目标架构为：.NET Standard 2.0

用Dapper对PostgreSQL调用进行封装。包括数据库访问操作，日志处理。

可扩展对象转换（PO与DO）,已引用了AutoMapper

## 目录说明

samples/Benchmark 对封装后的性能做了一些测试。

samples/WebDemo   webapi的调用方式

src/TianCheng.DAL.NpgByDapper 封装的源代码

## 如何在WebApi中使用

### 1.修改Startup.cs

在`Startup`的`ConfigureServices`中增加一行：`services.AddTianChengPostgres();`

示例

	```csharp

	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTianChengPostgres();   // 此处新增一行
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
	```

### 2. 数据库连接配置

  在appsettings.json中配置`DBConnection`节点。

  ``` json
    "TianCheng.DB": {
      "Assembly": [
          "WebDemo"
      ],
      "Connection": [
        {
          "Name": "default",
          "ConnectionString": "User ID=cheng;Password=123qwe;Host=192.168.0.16;Port=5432;Database=provider_manager;Pooling=true;"
        }
      ]
    }
  ```
  > 程序会自动查询程序集，并注册数据库操作对象，方便后续调用。Assembly 表示可以查询的程序集
  >
  
### 3. 对应的表操作需要继承 xxxOperation 。继承后需要重写属性TableName来指明数据库中的表名

  ```csharp

  public class MockGuidDAL : GuidOperation<MockGuidDB>
  {
    protected override string TableName { get; set; } = "mock_guid";
  }

  public class MockIntDal : IntOperation<MockIntDB>
  {
    protected override string TableName { get; set; } = "mock_serial";
  }

  ```

## 日志说明

通过`TianCheng.Log.Serilog`组件包使用[Serilog](https://serilog.net/)作为日志处理的工具。已添加操作对象`NpgLog`。按固定的配置写日志。

日志配置如下：

1. 控制台输出Warning级别以上的信息；
2. Debug窗口输出为全输出；
3. 文件输出为Warning级别以上的，文件名格式为`Logs/TianCheng.DBOperation-{Date}.txt`；(其实是直接调用TianCheng.DAL中的文件路径配置)
4. PostgreSQL数据库记录级别为Error，使用库为配置的default连接信息；存储表名称为system_logs。
5. 邮件发送级别为Fatal，收件账号可以通过`ToEmail`属性设置。

