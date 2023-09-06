using System.Collections.Generic;
using UnityEngine;

namespace Base.Core
{
    public class Parameter
    {
        private Dictionary<string, object> KeyValues { get; }

        public Parameter() => KeyValues = new Dictionary<string, object>();

        public static Parameter Create(string key, object value) =>
            new Parameter().Add(key, value);
    
        public Parameter Add(string key, object value)
        {
            Debug.Assert(!KeyValues.ContainsKey(key), $"Key {key} already in defined");
            KeyValues.Add(key, value);
            return this;
        }

        public T Get<T>(string key)
        {
            if (KeyValues.TryGetValue(key, out var value))
            {
                return (T)value;
            }

            return default;
        }
    }
}