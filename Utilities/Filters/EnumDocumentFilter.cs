using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace FoodieLionApi.Utilities.Filters;

public class EnumDoucumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        Dictionary<string, Type> dict = GetAllEnum();

        foreach (var item in swaggerDoc.Components.Schemas)
        {
            var property = item.Value;
            var typeName = item.Key;
            if (property.Enum is not null && property.Enum.Count > 0)
            {
                Type itemType = dict[typeName];
                List<OpenApiInteger> list = new();
                foreach (var val in property.Enum)
                {
                    list.Add((OpenApiInteger)val);
                }
                property.Description += DescribeEnum(itemType, list);
            }
        }
    }

    private static Dictionary<string, Type> GetAllEnum()
    {
        Assembly ass = Assembly.Load("foodie-lion-api");
        Type[] types = ass.GetTypes();
        Dictionary<string, Type> dict = new();

        foreach (Type item in types)
        {
            if (item.IsEnum)
            {
                dict.Add(item.Name, item);
            }
        }
        return dict;
    }

    private static string DescribeEnum(Type type, List<OpenApiInteger> enums)
    {
        var enumDescriptions = new List<string>();
        foreach (var item in enums)
        {
            var value = Enum.Parse(type, item.Value.ToString());
            enumDescriptions.Add($"{Enum.GetName(type, value)} = {item.Value}");
        }
        return $"<br/>{Environment.NewLine}{string.Join("<br/>" + Environment.NewLine, enumDescriptions)}";
    }
}
