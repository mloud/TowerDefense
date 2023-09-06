using System;

namespace Base.Core
{
    public class SmartProperty<T>
    {
        public Action<T> ChangedAction { get; set; }

        private T Value;

        public void Set(T value)
        {
            Value = value;
            ChangedAction?.Invoke(value);
        }

        public T Get() => Value;
        
        public void RegisterChangeListener(Action<T> listener, bool callListener = false)
        {
            ChangedAction += listener;
            if (callListener)
            {
                listener(Value);
            }
        }
        
        public void UnregisterChangeListener(Action<T> listener)
        {
            ChangedAction -= listener;
        }
    }
}