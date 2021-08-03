using System;
using UnityEngine;
using System.Linq;

[Serializable]
public struct FloatRange
{
    public float Min, Max;
    public FloatRange(float min, float max) => (Min, Max) = (min, max);
}

[Serializable]
public struct IntRange
{
    public int Min, Max;
    public IntRange(int min, int max) => (Min, Max) = (min, max);
}

[Serializable]
public struct ByteRange
{
    public byte Min, Max;
    public ByteRange(byte min, byte max) => (Min, Max) = (min, max);
}
