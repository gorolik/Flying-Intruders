using UnityEngine;

namespace Sources.Behaviour.Extensoins
{
    public static class GameFormulas
    {
        public static float CalculatePercentIncrease(float percent, float value) => 
            Mathf.Pow(1 + percent, value);
    }
}
