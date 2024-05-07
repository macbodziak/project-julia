using System;
using UnityEngine;

[Serializable]
public class EnumMappedArray<TValue, TEnum> where TEnum : Enum
{
    [SerializeField] private TValue[] content = null;
    [SerializeField] private TEnum enumType;

    public TValue this[int i]
    {
        get { return content[i]; }
    }

    public int Length
    {
        get { return content.Length; }
    }
}
