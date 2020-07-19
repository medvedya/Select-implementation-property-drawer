using System;
using UnityEngine;

public class SelectImplementationAttribute : PropertyAttribute
{
    public Type FieldType;

    public SelectImplementationAttribute(Type fieldType)
    {
        FieldType = fieldType;
    }
}
