﻿using System.Collections.Generic;

namespace TW.Vault.Model.Convert
{
    public static class ConvertUtil
    {
        public static T? GetOrNull<K, T>(IDictionary<K, T> dictionary, K key) where T : struct
        {
            if (dictionary.ContainsKey(key))
                return new T?(dictionary[key]);
            else
                return null;
        }

        public static void AddIfNotNull<K, T>(IDictionary<K, T> dictionary, K key, T? value) where T : struct
        {
            if (value.HasValue)
                dictionary.Add(key, value.Value);
        }
    }
}
