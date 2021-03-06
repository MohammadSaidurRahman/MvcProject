using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using MvcProject_Sayed.Models;
using MvcProject_Sayed.Models.ViewModel;
using AutoMapper;

namespace MvcProject_Sayed
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            Mapper.Initialize(config =>
            {
                config.CreateMap<CustomerVM, Customer>();
                config.CreateMap<Customer, CustomerVM>();
            });


        }

    }
}
