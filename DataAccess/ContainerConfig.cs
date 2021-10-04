using Autofac;
using DataAccess.Handlers;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AuthHandler>().As<IAuthHandler>();
            builder.RegisterType<MsSqlDataRepository>().As<IDataRepository>();
            builder.RegisterType<MsSqlUserRepository>().As<IControlRepository>();
            builder.RegisterType<ExcelExporter>().As<IExporter>();
            builder.RegisterType<WindowsAuthLogin>().As<ILogin>();
            builder.RegisterType<MsSqlRoleHandler>().As<IRoleHandler>();
            builder.RegisterType<MsSqlDBInit>().As<IDBInit>();
            builder.RegisterType<DataModel>();
            builder.RegisterType<ControlModel>();
            builder.RegisterType<DataHandlerFactory>();
            builder.RegisterType<ControlHandlerFactory>();

            return builder.Build();
        }
    }
}
