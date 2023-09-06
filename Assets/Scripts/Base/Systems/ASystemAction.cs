using System;

namespace OneDay.TowerDefense.Base.Systems
{
    public class SystemAction<T>
    {
        private Action<T> Listeners { get; set; }

        public void RegisterListener(Action<T> listener) =>
            Listeners += listener;
        
        public void UnregisterListener(Action<T> listener) =>
            Listeners -= listener;

        public void TriggerUpdate(T parameter) =>
            Listeners?.Invoke(parameter);
    }
    
    public class SystemAction<T1, T2>
    {
        private Action<T1, T2> Listeners { get; set; }

        public void RegisterListener(Action<T1, T2> listener) =>
            Listeners += listener;
        
        public void UnregisterListener(Action<T1, T2> listener) =>
            Listeners -= listener;

        public void TriggerUpdate(T1 parameter1, T2 parameter2) =>
            Listeners?.Invoke(parameter1, parameter2);
    }
    
    public class SystemAction<T1, T2, T3>
    {
        private Action<T1, T2, T3> Listeners { get; set; }

        public void RegisterListener(Action<T1, T2, T3> listener) =>
            Listeners += listener;
        
        public void UnregisterListener(Action<T1, T2, T3> listener) =>
            Listeners -= listener;

        public void TriggerUpdate(T1 parameter1, T2 parameter2, T3 parameter3) =>
            Listeners?.Invoke(parameter1, parameter2, parameter3);
    }
}