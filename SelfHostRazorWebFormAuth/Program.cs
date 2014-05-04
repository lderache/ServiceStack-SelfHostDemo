using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Logging;
using ServiceStack.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostRazorWebFormAuth
{
    class Program
    {
        //Define the Web Services AppHost
        public class AppHost : AppHostHttpListenerBase
        {
            public AppHost()
                : base("HttpListener SelfHost Demo", typeof(CarService).Assembly) { }

            public override void Configure(Funq.Container container)
            {
                Plugins.Add(new RazorFormat());

                Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                  new IAuthProvider[] { 
                    new CredentialsAuthProvider(), //HTML Form post of UserName/Password credentials
                  }));


                var userRep = new InMemoryAuthRepository();
                container.Register<IUserAuthRepository>(userRep);

                UserAuth userDemo = new UserAuth
                {
                    UserName = "demo"
                };

                userRep.CreateUserAuth(userDemo, "demo");
            }
        }

        static void Main(string[] args)
        {
            LogManager.LogFactory = new ConsoleLogFactory();

            var listeningOn = args.Length == 0 ? "http://*:8090/" : args[0];

            var appHost = new AppHost();

            appHost.Init();
            appHost.Start(listeningOn);

            Console.WriteLine("AppHost Created at {0}, listening on {1}",
                DateTime.Now, listeningOn);

            Console.ReadKey();
        }
    }
}
