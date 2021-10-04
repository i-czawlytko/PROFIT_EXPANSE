using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
namespace DataAccess
{
    public static class DataAccessApplication
    {
        private static IContainer container;
        static DataAccessApplication()
        {
            container = ContainerConfig.Configure();
        }

        public static DataModel GetDataModel()
        {
            using (var scope = container.BeginLifetimeScope())
            {
                return scope.Resolve<DataModel>();
            }
        }

        public static ControlModel GetControlModel()
        {
            using (var scope = container.BeginLifetimeScope())
            {
                return scope.Resolve<ControlModel>();
            }
        }
    }
}
