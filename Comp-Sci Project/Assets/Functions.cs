using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Functions
{
    ///<summary>
    ///Returns x * x * (3 - 2 * x), smoothing the function in and out. 
    ///</summary>
    public static float SmoothStep(float x)
    {
        x = Mathf.Clamp01(x);
        return x * x * (3 - 2 * x);
    }

    ///<summary>
    ///Remaps value from original to final bounds.
    ///</summary>
    public static float ReMap(float origFrom, float origTo, float from, float to, float value)
    {
        var invLerp = Mathf.InverseLerp(origFrom, origTo, value);
        invLerp = Mathf.Clamp01(invLerp);
        return Mathf.Lerp(from, to, invLerp);
    }

    ///<summary>
    ///Converts boolean to 1 if true and -1 if false.
    ///</summary>
    public static int BoolToInt(bool x, bool reverse = false)
    {
        return x != reverse ? 1 : -1;
    }

    ///<summary>
    ///Using a ref it counts to 1 which makes enums easier.
    ///</summary>
    public static bool CountTime(ref float t, float maxTime)
    {
        t += Time.deltaTime / maxTime;
        return t < 1;
    }
}
