using System;
using System.Collections.Generic;

namespace Workshop.Core.Helpers
{
    public static class DictionaryExtensions
    {
        public static Dictionary<TKey, TValue> ToNoneDuplicatedDictionary<T, TKey, TValue>(
            this IEnumerable<T> collection, 
            Func<T, TKey> keySource,
            Func<T, TValue> valueSource)
        {
            var result = new Dictionary<TKey, TValue>();
            
            foreach (var item in collection)
            {
                var key = keySource(item);
                
                if (result.ContainsKey(key))
                    continue;
                result[key] = valueSource(item);
            }

            return result;
        }
    }
}