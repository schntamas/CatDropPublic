using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Http.Dependencies;

namespace CatDrop.WebApi
{
    public class UnityResolver : IDependencyResolver
    {
        private bool _disposed = false;
        protected IUnityContainer Container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container is not initialized");
            }

            this.Container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Resolve(serviceType);
            }
            catch (ResolutionFailedException ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IDependencyScope BeginScope()
        {
            var childContainer = Container.CreateChildContainer();
            return new UnityResolver(childContainer);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                Container.Dispose();
            }

            _disposed = true;
        }
    }
}
