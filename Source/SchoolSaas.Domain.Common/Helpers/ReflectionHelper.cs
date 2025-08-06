using SchoolSaas.Domain.Common.Entities;
using System.Diagnostics;
using System.Reflection;

namespace SchoolSaas.Domain.Common.Helpers
{
    public static class TypeExtensions
    {
        public static bool IsBaseEntity(this Type type, out Type? idType)
        {
            for (var baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(BaseEntity<>))
                {
                    idType = baseType.GetGenericArguments()[0];
                    return true;
                }

            idType = null;
            return false;
        }

        public static bool IsBaseEntityTranslation(this Type type, out Type? idType)
        {
            for (var baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(TitledEntityTranslation<>))
                {
                    idType = baseType.GetGenericArguments()[0];
                    return true;
                }

            idType = null;
            return false;
        }

        public static bool IsTitledMultiLingualEntity(this Type type, out Type? translationType, out Type? idType)
        {
            for (var baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(TitledMultiLingualEntity<,>))
                {
                    translationType = baseType.GetGenericArguments()[0];
                    idType = baseType.GetGenericArguments()[1];
                    return true;
                }

            translationType = null;
            idType = null;
            return false;
        }
    }
    public static class MethodInfoExtensions
    {
        public static async Task<object?> InvokeAsync(this MethodInfo method, object? obj, params object[] parameters)
        {
            var result = method.Invoke(obj, parameters);
            if (result != null)
            {
                var task = (Task)result;
                await task.ConfigureAwait(false);
                var resultProperty = task.GetType().GetProperty("Result");
                return resultProperty?.GetValue(task);
            }
            return null;
        }
    }

    public static class ReflectionHelper
    {
        public static object? InvokeGenericMethod(Type type, object? instance, string methodName, Type[] genericTypes, params object[] arguments)
        {
            MethodInfo? method = type
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .SingleOrDefault(t => t.IsGenericMethod && t.Name == methodName);

            Debug.Assert(method != null);

            return method.MakeGenericMethod(genericTypes).Invoke(instance, arguments);
        }

        public static Task<object?> InvokeGenericMethodAsync(Type type, object? instance, string methodName, Type[] genericTypes, params object[] arguments)
        {
            MethodInfo? method = type
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .SingleOrDefault(t => t.IsGenericMethod && t.Name == methodName);

            Debug.Assert(method != null);

            return method.MakeGenericMethod(genericTypes).InvokeAsync(instance, arguments);
        }
    }
}
