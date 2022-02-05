using System.ComponentModel;
using System.Reflection;

namespace Shaper.Utility
{
    public static class ApiPaths
    {
        private const string root = "https://localhost:7219/api/";

        public enum ApiPath
        {
            [Description($"{root}Colors/")] Colors,
            [Description($"{root}Shapes/")] Shapes,
            [Description($"{root}Transparencies/")] Transparencies,
            [Description($"{root}Products/")] Products,
        }

        public static string GetEndpoint(this ApiPath path, int? id)
        {
            string endpoint = path.GetDescription();
            if (id == null)
            {
                return endpoint;
            }
            else
            {
                return endpoint+id.ToString();
            }
        }

        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }

            return null;
        }
    }
}
