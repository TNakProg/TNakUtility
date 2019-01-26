using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TNakUtility.Enums
{
    /// <summary>TypeSafeEnumBase</summary>
    public class TypeSafeEnumBase<TSelf, TKey> where TSelf : TypeSafeEnumBase<TSelf, TKey>
    {
        /// <summary>Dictionary{Key, Value}</summary>
        private static readonly Dictionary<TKey, TSelf> KeyValueDictionary = new Dictionary<TKey, TSelf>();

        static TypeSafeEnumBase()
        {
            // touch myself to initialize values
            // without this, Count might be 0 on first time
            var value =(typeof(TSelf).GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static |
                                                BindingFlags.DeclaredOnly)
                .Where(f => !f.IsLiteral)).FirstOrDefault()?.GetValue(null);
        }

        /// <summary>Initializes a new instance of the <see cref="TypeSafeEnumBase{TSelf, TKey}" /> class.</summary>
        /// <param name="key">The key.</param>
        protected TypeSafeEnumBase(TKey key)
         : this(key, key.ToString())
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TypeSafeEnumBase{TSelf, TKey}"/> class.</summary>
        /// <param name="key">The key.</param>
        /// <param name="name">The name.</param>
        protected TypeSafeEnumBase(TKey key, string name)
        {
            if (KeyValueDictionary.ContainsKey(key))
            {
                throw new ArgumentException($"The key [{key}] has been already registered.");
            }

            Key = key;
            Name = name;

            // Add {key, self} to dictionary
            KeyValueDictionary.Add(key, this as TSelf);
        }

        /// <summary>Name</summary>
        public string Name { get; }

        /// <summary>Key</summary>
        public TKey Key { get; }

        /// <summary>AllValues</summary>
        public static IReadOnlyCollection<TSelf> Values => KeyValueDictionary.Values;

        /// <summary>Count</summary>
        public static int Count => KeyValueDictionary.Count;

        /// <summary>Get value by key</summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>value</returns>
        public static TSelf FromKey(TKey key, TSelf defaultValue = null) => KeyValueDictionary.TryGetValue(key, out var ret) ? ret : defaultValue;

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => $" {Name}";
    }
}