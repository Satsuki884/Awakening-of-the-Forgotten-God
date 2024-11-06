using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using AFG;

[CustomEditor(typeof(PlayerData))]
public class PlayerDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerData script = (PlayerData)target;

        var buttons = typeof(PlayerData).GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(member => Attribute.IsDefined(member, typeof(ButtonAttribute)));

        foreach (var member in buttons)
        {
            var button = (ButtonAttribute)Attribute.GetCustomAttribute(member, typeof(ButtonAttribute));

            if (GUILayout.Button(member.Name))
            {
                var method = typeof(PlayerData).GetMethod(button.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
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