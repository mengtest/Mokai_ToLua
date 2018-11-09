using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtil
{
    public static void SayHello()
    {
        Debug.Log("Hello C# Warp ");
    }

    public static float TestFloat(float[] test)
    {
        float sum = 0;
        foreach (float f in test)
        {
            sum += f;
        }
        return sum;
    }
}
