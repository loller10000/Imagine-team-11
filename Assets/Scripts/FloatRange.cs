using UnityEngine;

/// <summary>
/// a property to get a random value inside that range,
/// and a constructor method to create a range for a give min and max.
/// </summary>

[System.Serializable]
public struct FloatRange
{
    public float min;
    public float max;
    public float RandomValue => Random.Range(min, max);

    public FloatRange(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public FloatRange GrowExtents(float extents) => 
        new(min - extents, max + extents);

    public FloatRange Shift(float shift) => 
        new(min + shift, max + shift);

    public static FloatRange PositionExtents(float position, float extents) => 
        new(position - extents, position + extents);
}