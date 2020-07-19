using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
public class SelectImplementationDrawer : PropertyDrawer
{
    private List<Type> _implementations;
    private int _implementationTypeIndex;
    bool isOpend = false;
    const float menuHeight = 18;
    const float openPadding = 10;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        //Menu 
        if (isOpend)
        {
            position.y += openPadding;
            float popupWidth = position.width * 0.75f;
            float buttonWidth = (position.width - popupWidth) / 2f;
            {
                if (_implementations == null || GUI.Button(new Rect(position.x + popupWidth, position.y, buttonWidth, menuHeight), EditorGUIUtility.IconContent("Refresh")))
                {
                    _implementations = GetImplementations((attribute as SelectImplementationAttribute).FieldType);
                }
                if (GUI.Button(new Rect(position.x + popupWidth + buttonWidth, position.y, buttonWidth, menuHeight), "Set"))
                {
                    var item = _implementations[_implementationTypeIndex];
                    if (item == null)
                    {
                        property.managedReferenceValue = null;
                    }
                    else
                    {
                        property.managedReferenceValue = Activator.CreateInstance(_implementations[_implementationTypeIndex]);
                    }
                    _implementationTypeIndex = 0;
                    isOpend = false;
                }
            }
            {
                _implementationTypeIndex = EditorGUI.Popup(new Rect(position.x, position.y, popupWidth, menuHeight), $"Implementation ({_implementations.Count - 1})",
                _implementationTypeIndex, _implementations.Select(impl => impl == null ? "null" : impl.FullName.Replace('+', '/')).ToArray());
            }
            position.y += menuHeight;

        }
        {
            const float openButtonWidth = 15;
            // Property
            {

                string typeName = "";
                if (string.IsNullOrEmpty(property.managedReferenceFullTypename))
                {
                    typeName = "null";
                }
                else
                {
                    var spaceIndex = property.managedReferenceFullTypename.IndexOf(" ") + 1;
                    if (spaceIndex != 0)
                    {
                        typeName = property.managedReferenceFullTypename.Substring(spaceIndex, property.managedReferenceFullTypename.Length - spaceIndex);
                    }
                    else
                    {
                        typeName = property.managedReferenceFullTypename;
                    }
                }
                EditorGUI.PropertyField(new Rect(position.x, position.y, position.width - openButtonWidth, EditorGUI.GetPropertyHeight(property)), property, new GUIContent($"{property.displayName} ({typeName})"), true);

            }
            // Open menu button
            { 
                const float openButtonHeight = 18;
                isOpend = GUI.Toggle(new Rect(position.x + position.width - openButtonWidth, position.y, openButtonWidth, openButtonHeight), isOpend, "i", "Button");
            }
        }
        EditorGUI.EndProperty();


    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var height = EditorGUI.GetPropertyHeight(property);
        if (isOpend)
        {
            height += menuHeight + openPadding * 2;
        }
        return height;
    }

    private static List<Type> GetImplementations(Type interfaceType)
    {
        List<Type> resTypes = new List<Type>();
        resTypes.Add(null);
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());
        resTypes.AddRange(types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract && !p.IsSubclassOf(typeof( UnityEngine.Object))));
        return resTypes;
    }
}