using System.Collections.Generic;
using UnityEngine;

namespace Game.Utility
{
    public static class RandomUtils
    {
        public static Vector3 RandomPointInAnnulus(Vector3 center, float minRadius, float maxRadius)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);

            float radius = Mathf.Sqrt(Random.Range(minRadius * minRadius, maxRadius * maxRadius));

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            return center + new Vector3(x, 0f, z);
        }
        
        public static T GetRandomElement<T>(this IReadOnlyList<T> list) 
            => list[Random.Range(0, list.Count)];
    }
}