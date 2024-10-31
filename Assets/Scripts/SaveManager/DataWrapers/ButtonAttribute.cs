using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ButtonAttribute : PropertyAttribute
{
    public string MethodName { get; private set; }

    public ButtonAttribute(string methodName)
    {
        MethodName = methodName;
    }
}