using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using AFG;

[CustomEditor(typeof(CharacterDataHolder))]
public class CharacterDataHolderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CharacterDataHolder script = (CharacterDataHolder)target;

        var buttons = typeof(CharacterDataHolder).GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(member => Attribute.IsDefined(member, typeof(ButtonAttribute)));

        foreach (var member in buttons)
        {
            var button = (ButtonAttribute)Attribute.GetCustomAttribute(member, typeof(ButtonAttribute));

            if (GUILayout.Button(member.Name))
            {
                var method = typeof(CharacterDataHolder).GetMethod(button.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                if (method != null)
                {
                    method.Invoke(script, null);
                }
                else
                {
                    Debug.LogWarning($"Method {button.MethodName} not found");
                }
            }
        }
    }
}