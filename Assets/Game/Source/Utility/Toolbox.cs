using UnityEngine;

namespace Game.Utility
{
    public static class Toolbox
    {
        public static Vector3 GetRandomXZPositionInRange(Vector3 center, float range)
        {
            var offset = Random.insideUnitSphere * range;
            offset.y = 0f;
            return center + offset;
        }
    }
}