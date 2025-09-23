using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

namespace Utilities
{
    public static class EnumUtilities
    {
        public static string StringValue(this Enum value) {
            FieldInfo field = value.GetType().GetField(value.ToString());
            
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
