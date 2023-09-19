using Godot;
using System.Reflection;

namespace Attributes
{
    public static class Extension
    {
        public static void WireNodes(this Node node)
        {
            PropertyInfo[] info = node.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (PropertyInfo property in info)
            {
                NodeAttribute attr = property.GetCustomAttribute<NodeAttribute>();
                if (attr != null)
                {
                    property.SetValue(node, node.GetNode(attr.nodePath));
                }
            }
        }
    }
}