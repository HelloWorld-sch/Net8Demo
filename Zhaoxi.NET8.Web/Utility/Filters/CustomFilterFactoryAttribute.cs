using Microsoft.AspNetCore.Mvc.Filters;

namespace Zhaoxi.NET8.Web.Utility.Filters
{
    public class CustomFilterFactoryAttribute : Attribute, IFilterFactory
    {
        private readonly Type _Type;
        public CustomFilterFactoryAttribute(Type type)
        {
            _Type = type;
        }


        public bool IsReusable => true;

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        { 
            object oInstance = serviceProvider.GetService(_Type); 
            return oInstance as IFilterMetadata;
        }
    }
}
