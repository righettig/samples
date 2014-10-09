using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;
using Mvc5DIFriendly.Models;
using Mvc5DIFriendly.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Mvc5DIFriendly.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Mvc5DIFriendly.App_Start.NinjectWebCommon), "Stop")]

namespace Mvc5DIFriendly.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IFoo>().To<Foo>();

            kernel.Bind<DbContext>().To<ApplicationDbContext>().InRequestScope();
            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>().InRequestScope();

            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();

            kernel.Bind<ApplicationUserManager>().ToSelf().InRequestScope();
            
            kernel.Bind<IAuthenticationManager>().ToMethod(
                c =>
                    HttpContext.Current.GetOwinContext().Authentication).InRequestScope();

            kernel.Bind<ISecureDataFormat<AuthenticationTicket>>().To<SecureDataFormat<AuthenticationTicket>>().InRequestScope();
            kernel.Bind<IDataSerializer<AuthenticationTicket>>().To<TicketSerializer>().InRequestScope();
            
            kernel.Bind<ITextEncoder>().To<Base64TextEncoder>().InRequestScope();
            //kernel.Bind<ITextEncoder>().To<Base64UrlTextEncoder>().InRequestScope();

            kernel.Bind<IDataProtector>().ToMethod(
                ctx => new DpapiDataProtectionProvider().Create("ASP.NET Identity")).InRequestScope();
        }        
    }
}
