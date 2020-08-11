namespace WebApi1.Resource
{
    using System;

    /// <summary>
    /// Key/Value1,Value1
    /// </summary>
    [Serializable]
    public class KeyValues<TKey, TValue, TValue1>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public KeyValues() { }

        /// <summary>
        /// ctor
        /// </summary>
        public KeyValues(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// ctor
        /// </summary>
        public KeyValues(TKey key, TValue value, TValue1 value1)
        {
            Key = key;
            Value = value;
            Value1 = value1;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="model"></param>
        public KeyValues(KeyValues<TKey, TValue, TValue1> model)
        {
            if (model != null)
            {
                this.Key = model.Key;
                this.Value = model.Value;
                this.Value1 = model.Value1;
            }
        }

        /// <summary>
        /// Key
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Value1
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// value2
        /// </summary>
        public TValue1 Value1 { get; set; }
    }

    /// <summary>
    /// Key/Value
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class KeyValues<TKey, TValue> : KeyValues<TKey, TValue, object>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public KeyValues() : base() { }

        /// <summary>
        /// ctor
        /// </summary>
        public KeyValues(TKey key, TValue value) : base(key, value)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="model"></param>
        public KeyValues(KeyValues<TKey, TValue> model) : base(
            model == null ? default(TKey) : model.Key,
            model == null ? default(TValue) : model.Value)
        {
        }
    }

    /// <summary>
    /// Key/Value
    /// </summary>
    [Serializable]
    public class KeyValues<TValue> : KeyValues<string, TValue>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public KeyValues() : base() { }

        /// <summary>
        /// ctor
        /// </summary>
        public KeyValues(string key, TValue value) : base(key, value)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="model"></param>
        public KeyValues(KeyValues<TValue> model) : base(
            model == null ? null : model.Key,
            model == null ? default(TValue) : model.Value)
        {
        }
    }

    /// <summary>
    /// Key/Value
    /// </summary>
    [Serializable]
    public class KeyValues : KeyValues<string>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public KeyValues() : base() { }

        /// <summary>
        /// ctor
        /// </summary>
        public KeyValues(string key, string value) : base(key, value)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="model"></param>
        public KeyValues(KeyValues model) : base(
            model == null ? null : model.Key,
            model == null ? null : model.Value)
        {
        }
    }
}