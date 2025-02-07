﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Streamye.DesignPattern.Builder;
using Microsoft.Streamye.DesignPattern.Builder.component;
using Microsoft.Streamye.DesignPattern.Builder.component.Impl;
using Microsoft.Streamye.DesignPattern.Chain;
using Microsoft.Streamye.DesignPattern.Chain.Manager;
using Microsoft.Streamye.DesignPattern.Decorator;
using Microsoft.Streamye.DesignPattern.Decorator.Impl;
using Microsoft.Streamye.DesignPattern.Factory;
using Microsoft.Streamye.DesignPattern.IOC;
using Microsoft.Streamye.DesignPattern.IOC.Services;
using Microsoft.Streamye.DesignPattern.Iterator;
using Microsoft.Streamye.DesignPattern.Iterator.Impl;
using Microsoft.Streamye.DesignPattern.Meditor.Group;
using Microsoft.Streamye.DesignPattern.Observer;
using Microsoft.Streamye.DesignPattern.Observer.Impl;
using Microsoft.Streamye.DesignPattern.Strategy;
using Microsoft.Streamye.DesignPattern.Template;
using Microsoft.Streamye.DesignPattern.Template.Impl;
using Microsoft.Streamye.DesignPattern.WebAPI;
using Microsoft.Streamye.DesignPattern.WebAPI.Facade;
using Microsoft.Streamye.DesignPattern.WebApiMiddleware;
using Microsoft.Streamye.DesignPattern.WebApiMiddleware.Chains;
using ConfigurationBuilder = Microsoft.Streamye.DesignPattern.Configuration.ConfigurationBuilder;
using HttpContext = Microsoft.Streamye.DesignPattern.WebAPI.HttpContext;

namespace Microsoft.Streamye.DesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 工厂模式
            { 
            // var configBuilder = new ConfigurationBuilder();
            // configBuilder.AddJsonFile("shape.json", false, true);
            // var configurationRoot = configBuilder.Build();
            //
            // var services = new ServiceCollection();
            // services.AddSingleton<ShapeFactory>();
            // services.AddSingleton<IConfiguration>(configurationRoot);
            //
            // var serviceProvider=services.BuildServiceProvider();
            //
            // var shapeFactory= serviceProvider.GetService<ShapeFactory>();
            // shapeFactory.getShage("circle").Draw(); //1.命令行参数 2.配置文件中读
            }
            #endregion

            #region 策略模式
            
            // IStrategy strategy = new AddStrategy();
            // Arithmetic arithmetic = new Arithmetic(strategy);
            // Console.WriteLine(arithmetic.doMath(5, 3));
            //
            // //问题：客户端得创建这个strategy , 不符合开闭原则 =》 工厂模式 , 策略被使用的时候是一种行为（策略模式），但是这种策略本身又是一个对象（工厂模式）
            // StrategyFactory strategyFactory = new StrategyFactory();
            // IStrategy strategy1 = strategyFactory.GetStrategy(args[1]); //命令行参数中读，或者配置文件中读
            // // Arithmetic arithmetic = new Arithmetic(strategy1);
            //
            #endregion

            #region IOC容器
            
            // IOCFactory iocFactory = new IOCFactory();
            // Object o = iocFactory.GetObject<Canon>();
            //
            #endregion

            #region 构造者模式
                
            // //正常操作：
            // AbstractCPU cpu = new MSCPU();
            // //AbstractCPU cpu = new MSCPU(arg1,arg2,arg3...arg10);
            // AbstractFrame frame = new MSFrame();
            // AbstractMemory memory = new MSMemory();
            // Computer computer = new Computer();
            // computer.Cpu = cpu;
            // computer.Frame = frame;
            // computer.Memory = memory;
            // //问题：1.客户端代码会非常多，如果cpu和frame和memory都需要10个零部件，那么这段代码就会非常复杂 =》 封装
            //
            
            // IComputerBuilder computerBuilder = new ComputerBuilder();
            // Computer computer1 = computerBuilder.BuildMemory().BuildFrame().BuildCPU().Build();
            // //何时用抽象类或者接口？ 1.一般关注对象，则用抽象类（CPU,Memory,Frame） 2.如果不关心对象的属性，只关心对象的行为，用接口；
            //
            // // 真实案例：IConfigurationBuilder
            // IConfigurationBuilder Builder = new ConfigurationBuilder();
            // // 加载json文件
            // // 好处：
            // // 1.json,xml,ini,我都不care,而且你完全自己的实现自己的代码，跟我的IConfigurationBuilder丝毫没有耦合
            // // 2.如果json的实现有多复杂，那是json的 JsonConfigurationProvider的问题
            // Builder.AddJsonFile("appsettings.json");
            // // 创建配置对象
            // IConfiguration configuration = Builder.Build();
            
            // 其他案例：
            // IHostBuilder
            // IApplicationBuilder
            
            #endregion
            
            #region 装饰器模式
            
            // IPay pay = new CMDPay();
            // // pay.Pay();
            // //问题：想添加pay完之后 发送短信的功能，不仅发送短信还想发送 邮件
            // //两种方案： 1.继承方式（会有里氏替换问题） 2.组合方式 3.C#独有的拓展方法 4.代理模式
            //
            // IPay pay1 = new SmsCMDPayDecorator(pay);
            // pay1.Pay(); //组合方式：没有覆盖父类，额外的好处：如果又想发短信，又想发邮件
            //
            // IPay pay2 = new MailCMDPayDecorator(pay1); //这种组合的方式
            // pay2.Pay(); 
            //
            // // C#拓展
            // pay.SendSms();
            // // pay.SendMail();
            //
            // //问题：
            // //实例：IServiceCollection, IApplicationBuilder
            
            #endregion

            #region 代理模式
            //
            // 问题：与装饰器模式的区别，不希望暴露底层的实现, 而且可以加强多个对象的功能， 而装饰器更希望自由组装
            //
            #endregion

            #region 责任链模式
            //
            // M1Manager m1Manager = new M1Manager();
            // M2Manager m2Manager = new M2Manager();
            // CvpManager cvpManager = new CvpManager();
            //
            // //形成责任链
            // m1Manager.NextAbstractManager = m2Manager;
            // m2Manager.NextAbstractManager = cvpManager;
            //
            // ProductAuditRequest request = new ProductAuditRequest();
            // request.Money = 1000*1000*5;
            // request.ProductName = "canon";
            //
            // // m1Manager.AuditProduct(request);
            //
            // //问题：1. 如果新增一个处理器，则客户端代码得改 =》 List
            // //真实情况类似：A权限校验， B权限校验， C权限校验 
            //
            // ProductAuditBuilder productAuditBuilder = new ProductAuditBuilder();
            // productAuditBuilder.AddM1Manager();
            // productAuditBuilder.AddManager(m2Manager);
            // productAuditBuilder.AddManager(cvpManager);
            //
            // //两种拓展的方法 ： 1. client中修改 2. Extension中拓展 ，真实的是在 IOC容器里面拿的对象
            // // productAuditBuilder.AddManager(new EVPManager());
            // productAuditBuilder.AddEVPManager(); 
            //
            // ProductAudit productAudit = productAuditBuilder.Build();
            // AbstractManager abstractManager = productAudit.GetManager();
            //
            // request.Money = 1000*1000*500;
            // request.ProductName = "canon2";
            // abstractManager.AuditProduct(request);
            //
            // //问题：1.why 需要builder来做？  2.感觉所有的业务都可以这么做？
            // //假如是同类型的行为，比如我都是权限校验！！

            #endregion

            #region 迭代器模式
            //目的：1.不想暴露内部结构（这一点跟代理模式一样） 2. 统一访问方法，不管你是map,list, array 

            // List list = new List();
            // string[] names = list.GetNames();
            //
            // foreach (var name in names)
            // {
            //     Console.WriteLine("name:"+name);
            // }
            //
            // //iterator方式访问
            // IIterator iterator = list.GetIterator();
            // while (iterator.HasNext())
            // {
            //     Console.WriteLine("name:"+iterator.Next());
            // }

            //
            // IDictionary<string,string> dictionary = new Dictionary<string,string>();
            // dictionary["AA"] = "zhangsan";
            // dictionary["BB"] = "lisi";
            // IEnumerator enumerator = dictionary.GetEnumerator();
            // while (enumerator.MoveNext())
            // {
            //     Console.WriteLine(enumerator.Current);
            // }

            #endregion

            #region webApi middleware
            //责任链的真实使用
            //都是对httpcontext处理，返回也相同
            
            // //假如不用builder
            // IApplication application = new IApplication();
            // AbstractMiddleware middleware1 = new AuthenticationMiddleware();
            // AbstractMiddleware middleware2 = new AuthorizationMiddleware();
            // application.AddMiddleware(middleware1);
            // application.AddMiddleware(middleware2);
            // AbstractMiddleware middleware = application.GetMiddleware();
            //
            // HttpRequest request = new HttpRequest();
            // request.requestUrl = "microsoft.com";
            // middleware.HandleHttpRequest(request);
            //
            // //使用applicationBuilder
            // IApplicationBuilder applicationBuilder = new IApplicationBuilder();
            // Startup startup = new Startup();
            // startup.Configure(applicationBuilder);

            #endregion

            #region 模板方法
            //目的：如果有共同的操作大流程，就可以放到父亲中， 提升代码的可维护性
            //将日志存储到云上
            //问题：如果我希望建立一个统一的标准，如果之后还有其他CloudServer来用

            // ICloudServer cloudServer = new ESCloudServer();
            // cloudServer.SaveLog();
            
            //再封一下，封装个工厂
            // ICloudServer cloudServer1 = CloudServerFactory.GetCloudServer("kusto");
            // cloudServer1.SaveLog();
            
            #endregion

            #region webapi
            //获得类型
            // Type type = typeof(CanaryCheckController);
            // //创建对象
            // Object o = Activator.CreateInstance(type);
            // //获得方法
            // MethodInfo methodInfo = type.GetMethod("CheckVM");
            // //调用方法
            // // methodInfo.Invoke(o, new object[] { });
            //
            // //对上面进行封装
            // Endpoint endpoint = new Endpoint();
            // endpoint.Controller = o;
            // endpoint.methodInfo = methodInfo;
            // endpoint.parameterInfos = methodInfo.GetParameters();
            //
            // EndpointHandler handler = new EndpointHandler();
            // handler.HandlerEndpoint(endpoint, new object[]{});
            //
            // //问题：有很多个endpoint , 得全部加载好
            // // 1、构造endpoint集合
            // EndpointRouteBuilder endpointRouteBuilder = new EndpointRouteBuilder();
            // endpointRouteBuilder.MapControllers();
            // EndpointDataSources endpointDataSources = endpointRouteBuilder.Build();
            //
            // // 2 路由匹配(路由中间件)
            // EndpointRoute endpointRoute = new EndpointRoute();
            // endpointRoute.endpointDataSources = endpointDataSources;
            // HttpContext httpContext = new HttpContext();
            // httpContext.requestUrl = "canary/checkvm";
            // Endpoint endpoint1 = endpointRoute.Match(httpContext);
            //
            // // 3、执行端点
            // EndpointHandler endpointHandler = new EndpointHandler();
            // object result1 = endpointHandler.HandlerEndpoint(endpoint1, new object[] { });
            //
            // // 4、把结果响应到客户端
            // // JSONView jSONView = new JSONView();
            // // jSONView.Render(result1, httpContext);*/
            //
            //
            // //问题：上面步骤再进行一次封装
            // EndpointMiddlewareFacade middlewareFacade = new EndpointMiddlewareFacade();
            // middlewareFacade.Invoke(httpContext);

            //问题：再进一步，集合Iapplicationbuilder, 添加一个 UseEndpointMiddleware
            #endregion

            #region Configuration

            // Configuration.Configuration configuration = new Configuration.Configuration();
            // configuration.Set("aa","bbb");
            // Console.WriteLine(configuration["aa"]);
            //
            //问题：客户端得改，违背开闭原则，如何从json文件中读取？ 会有不同的数据源：xml,ini,json
            //
            
            // ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            // configurationBuilder.AddJsonFile("aa.json");
            // Configuration.Configuration configuration = configurationBuilder.Build();
            // Console.WriteLine(configuration["aaaa"]);

            #endregion


            #region 观察者模式
            //目的： 解决：1对多的通信问题
            // Baby baby = new Baby();
            // baby.AddObserver(new Parents());
            // baby.AddObserver(new GrandParents());
            // baby.Cry();
            //
            // //问题baby生病也想通知别人
            // baby.Health = "生病";
            
            //不想要Baby这种事件实体 =》 只有事件 ： EventBus 
            // EventBus eventBus = new EventBus();
            // eventBus.AddObserver(new GrandParentsEventHandler());
            //
            // //构造事件：
            // BabyCryEvent babyCryEvent = new BabyCryEvent("baby cry event");
            // eventBus.PublishEvent(babyCryEvent);

            #endregion

            #region 中介模式

            Student student = new Student();
            Teacher teacher = new Teacher();
            Parent parent = new Parent();
            Meditor.Meditor meditor = new Meditor.Meditor();
            meditor.AddPeople(student);
            meditor.AddPeople(teacher);
            meditor.AddPeople(parent);

            student.Meditor = meditor;
            teacher.Meditor = meditor;
            parent.Meditor = meditor;
            
            student.Send("student say something");

            #endregion
        }
        
    }
}