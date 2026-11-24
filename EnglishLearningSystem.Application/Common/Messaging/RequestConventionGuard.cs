using MediatR;
using System.Reflection;

namespace EnglishLearningSystem.Application.Common.Messaging
{
    /// <summary>
    /// Fails startup when a request does not implement <see cref="ICommand"/>, <see cref="ICommand{TResponse}"/>
    /// or <see cref="IQuery{TResponse}"/>. Otherwise its response may not be a Result, and DI silently drops
    /// ValidationBehavior for it (its validator never runs).
    /// </summary>
    internal static class RequestConventionGuard
    {
        private static readonly Type[] AllowedGenericMarkers = [typeof(ICommand<>), typeof(IQuery<>)];

        public static void EnsureValid(Assembly assembly)
        {
            var invalid = assembly.GetTypes()
                .Where(t => t is { IsAbstract: false, IsInterface: false, IsGenericTypeDefinition: false } && typeof(IBaseRequest).IsAssignableFrom(t))
                .Where(t => !IsMarked(t))
                .Select(t => t.FullName)
                .ToList();

            if (invalid.Count > 0)
            {
                throw new InvalidOperationException(
                    "Requests must implement ICommand, ICommand<T> or IQuery<T> (response must be Result / Result<T>, " +
                    "otherwise ValidationBehavior is skipped): " + string.Join(", ", invalid));
            }
        }

        private static bool IsMarked(Type type) =>
            typeof(ICommand).IsAssignableFrom(type) ||
            type.GetInterfaces().Any(i => i.IsGenericType && AllowedGenericMarkers.Contains(i.GetGenericTypeDefinition()));
    }
}
