using UnityEngine;

public class RangeIntAttribute : PropertyAttribute
{
    public int min;
    public int max;

    public RangeIntAttribute(int min, int max)
    {
        this.min = min;
        this.max = max;
    }
}