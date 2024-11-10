using Autofac.Core;
using System.Reflection;

namespace Zhaoxi.NET8.AutofacExtension
{
    public class CustomPropertySelector : IPropertySelector
    {
        /// <summary>
        /// 用来决定哪个属性是需要做属性注入的
        /// </summary>
        /// <param name="propertyInfo">属性</param>
        /// <param name="instance">对象</param>
        /// <returns></returns> 
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            bool bResult = propertyInfo
                            .CustomAttributes
                            .Any(c => c.AttributeType == typeof(CusotmSelectAttribute));

            return bResult; 
        }
    }
}
