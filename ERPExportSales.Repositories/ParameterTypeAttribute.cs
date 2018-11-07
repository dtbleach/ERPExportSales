

namespace ERPExportSales.Repositories
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class ParameterTypeAttribute : Attribute
    {
        public ParameterTypeAttribute()
        {
        }

        public ParameterTypeAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; set; }

        public string StoreType { get; set; }
    }
}
