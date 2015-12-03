using System;

namespace TaskManager.Node.Helper
{
    /// <summary>
    /// 应用程序域加载者
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AppDomainLoader<T> where T : class
    {
        /// <summary>
        /// 加载应用程序域，获取相应实例
        /// </summary>
        /// <param name="dllPath"></param>
        /// <param name="classFullName"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public T Load(string dllPath, string classFullName, out AppDomain domain)
        {
            var setup = new AppDomainSetup
            {
                ShadowCopyFiles = "true",
                ApplicationBase = System.IO.Path.GetDirectoryName(dllPath)
            };
            domain = AppDomain.CreateDomain(System.IO.Path.GetFileName(dllPath), null, setup);
            AppDomain.MonitoringIsEnabled = true;
            return (T)domain.CreateInstanceFromAndUnwrap(dllPath, classFullName);
        }
        /// <summary>
        /// 卸载应用程序域
        /// </summary>
        /// <param name="domain"></param>
        public void UnLoad(AppDomain domain)
        {
            AppDomain.Unload(domain);
            domain = null;
        }
    }
}
