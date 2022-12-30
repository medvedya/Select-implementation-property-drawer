using System;
using UnityEngine;

public class SelectImplementationAttribute : PropertyAttribute
{
    public Type FieldType;
    public bool ShowNamespace;
    public bool ShowParentClass;
    public SelectImplementationAttribute(Type fieldType, bool showNamespace = false, bool showParentClass = false)
    {
        FieldType = fieldType;
        ShowNamespace = showNamespace;
        ShowParentClass = showParentClass;
    }
}
