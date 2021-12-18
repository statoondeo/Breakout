using System;
using System.Collections.Generic;

namespace GameNameSpace
{
    public class Services
    {
        private static readonly Lazy<Services> mInstance;
        private readonly IDictionary<Type, IService> RegisteredServices;

        static Services()
		{
            mInstance = new Lazy<Services>(() => new Services());
        }

        private Services()
        {
            RegisteredServices = new Dictionary<Type, IService>();
        }

        public static Services Instance
        {
            get
            {
                return mInstance.Value;
            }
        }

        public T Register<T>(T service) where T : IService
        {
            RegisteredServices.Add(typeof(T), service);
            return (service);
        }

        public T Get<T>() where T : class
        {
            return (RegisteredServices.ContainsKey(typeof(T)) ? RegisteredServices[typeof(T)] as T : null);
        }
    }
}
