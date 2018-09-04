using Autofac;
using Autofac.Integration.WebApi;
using SSE.Lottery.Service;
using System.Reflection;
using System.Web.Http;

namespace SSE.Lottery.WebApi.App_Start
{
    public class IocConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterDependencies(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public static IContainer RegisterDependencies(ContainerBuilder builder)
        {
            // Register you web Api controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<LotteryManager>().As<ILotteryManager>().InstancePerRequest();

            return builder.Build();
        }
    }
}