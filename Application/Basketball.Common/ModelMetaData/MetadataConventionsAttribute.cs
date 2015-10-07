using System;

namespace Basketball.Common.ModelMetaData
{
    public class MetadataConventionsAttribute : Attribute
    {
        public Type ResourceType { get; set; }
    }
}