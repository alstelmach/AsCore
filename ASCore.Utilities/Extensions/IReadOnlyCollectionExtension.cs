using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASCore.Utilities.Extensions
{
    public static class IReadOnlyCollectionExtension
    {
        public static void ForEach<T>(this IReadOnlyCollection<T> collection, params Action<T>[] actions)
        {
            foreach (var item in collection)
            {
                foreach (var action in actions)
                {
                    action(item);
                }
            }
        }
        
        public static async Task ForEachAsync<T>(this IReadOnlyCollection<T> collection, params Func<T, Task>[] actions)
        {
            foreach (var item in collection)
            {
                foreach (var action in actions)
                {
                    await action(item);
                }
            }
        }
    }
}
