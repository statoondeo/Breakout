using System;
using System.Collections.Generic;

namespace GameNameSpace
{
    public class ServiceLocator
    {
        private static readonly Lazy<ServiceLocator> instance;
        private readonly IDictionary<Type, object> registeredServices;

        static ServiceLocator()
		{
            instance = new Lazy<ServiceLocator>(() => new ServiceLocator());
        }

        private ServiceLocator()
        {
            registeredServices = new Dictionary<Type, object>();
        }

        public static ServiceLocator Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public T Register<T>(T service) where T : class
        {
            registeredServices.Add(typeof(T), service);
            return (service);
        }

        public T Get<T>() where T : class
        {
            return (registeredServices[typeof(T)] as T);
        }
    }
}
