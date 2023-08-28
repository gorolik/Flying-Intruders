using UnityEngine;

namespace Sources.Behaviour.Extensions
{
    public static class GameFormulas
    {
        public static float CalculatePercentIncrease(float percent, float value) => 
            Mathf.Pow(1 + percent, value);
    }
}
