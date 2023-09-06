using System;
using UnityEngine;

namespace Base.Core
{
    public class MissingComponentException<T>: Exception where T : Component
    {
        public MissingComponentException(GameObject go) 
            : base($"Component of type {typeof(T)} is missing on GameObject {go.name}")
        { }
    }
}