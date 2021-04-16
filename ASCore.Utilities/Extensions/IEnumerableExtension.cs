using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASCore.Utilities.Extensions
{
    public static class IEnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, params Action<T>[] actions)
        {
            foreach (var item in enumerable)
            {
                foreach (var action in actions)
                {
                    action(item);
                }
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> enumerable, params Func<T, Task>[] actions)
        {
            foreach (var item in enumerable)
            {
                foreach (var action in actions)
                {
                    await action(item);
                }
            }
        }

        public static async Task<IDictionary<TKey, TValue>> ToDictionaryAsync<TInput, TKey, TValue>(
            this IEnumerable<TInput> enumerable,
            Func<TInput, TKey> keySelector,
            Func<TInput, Task<TValue>> asyncValueSelector)
        {
            var result = new Dictionary<TKey, TValue>();
            
            foreach(var item in enumerable)
            {
                var key = keySelector(item);
                var value = await asyncValueSelector(item);
                result.Add(key, value);
            }

            return result;
        }
    }
}
