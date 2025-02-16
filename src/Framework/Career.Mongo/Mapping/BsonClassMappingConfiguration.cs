using System.Reflection;

namespace Career.Mongo.Mapping;

public static class BsonClassMappingConfiguration
{
    public static void ApplyConfigurationsFromAssembly(Type assemblyPointerType)
    {
        ApplyConfigurationsFromAssembly(assemblyPointerType.Assembly);
    }

    public static void ApplyConfigurationsFromAssembly(Assembly assembly)
    {
        List<Type> mappings = assembly.GetTypes()
            .Where(t => t.BaseType != null 
                        && t.BaseType.IsGenericType 
                        && t.BaseType.GetGenericTypeDefinition() == typeof(MongoDbClassMap<>))
            .ToList();

        foreach (Type mapping in mappings)
        {
            ConstructorInfo ctor = mapping.GetConstructor(Type.EmptyTypes);
            ctor?.Invoke(new object[] { });
        }
    }
}