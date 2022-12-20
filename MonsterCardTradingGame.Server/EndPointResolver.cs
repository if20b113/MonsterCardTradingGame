using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Server
{
    public static class EndPointResolver
    {

        private static Assembly[] GetSolutionAssemblies()
        {

            var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*dll")
                .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));

            return assemblies.ToArray();

        }


        public static IEndpointcontroller[] FindEndpointControllers()
        {
            var assemblies = GetSolutionAssemblies();


            List<Type> types = assemblies.SelectMany(x => x.GetTypes()).Where(x => typeof(IEndpointcontroller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList();


            List<IEndpointcontroller> endpoints = new List<IEndpointcontroller>();

            foreach(var type in types)
            {
                IEndpointcontroller Instance = (IEndpointcontroller)Activator.CreateInstance(type);

                endpoints.Add(Instance);

            }

            return endpoints.ToArray();

        }
    }
}
