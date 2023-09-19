using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class NodeAttribute : Attribute
    {
        public string nodePath { get; set; }
        public NodeAttribute(string nodePath)
        {
            this.nodePath = nodePath;
        }
    }
}