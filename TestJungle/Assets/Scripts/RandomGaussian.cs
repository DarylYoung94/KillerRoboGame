using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For more info go to https://www.alanzucconi.com/2015/09/16/how-to-sample-from-a-gaussian-distribution/

public class RandomGaussian
{
    public static float GetGaussian(float mean, float standard_deviation, float min, float max)
    {
        float x;
        do {
            x = MarsagliaPolarGaussian(mean, standard_deviation);
        }  while (x < min || x > max);

        return x;
    }

    private static float MarsagliaPolarGaussian(float mean, float standard_deviation)
    {
        return mean + MarsagliaPolar() * standard_deviation;
    }

    private static float MarsagliaPolar()
    {
        float v1, v2, s;
        do {
            v1 = 2.0f * Random.Range(0f,1f) - 1.0f;
            v2 = 2.0f * Random.Range(0f,1f) - 1.0f;
            s = v1 * v1 + v2 * v2;
        } while (s >= 1.0f || s == 0f);
    
        s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);
    
        return v1 * s;
    }

}
