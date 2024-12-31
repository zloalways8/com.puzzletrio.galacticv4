using System;
using System.Collections.Generic;
using Core.Api;

namespace Core
{
    public static class fghjjdfh
    {
        private static Dictionary<Type, jkdgh> _services = new Dictionary<Type, jkdgh>();

        public static void Bind<T>(T service) where T : class, jkdgh
        {
            if (_services.ContainsKey(typeof(T)))
                return;

            _services[typeof(T)] = service;
        }

        public static T dfghjjdfgh<T>() where T : class, jkdgh => 
            _services.ContainsKey(typeof(T)) ? (T)_services[typeof(T)] : null;
    }
}