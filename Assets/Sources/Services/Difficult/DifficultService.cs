using UnityEngine;

namespace Sources.Services.Difficult
{
    public class DifficultService : IDifficultService
    {
        public float DifficultPerSecond { get; }
        public float MaxDifficultValue { get; set; }

        public DifficultService(float difficultPerSecond, float maxDifficultValue)
        {
            DifficultPerSecond = difficultPerSecond;
            MaxDifficultValue = maxDifficultValue;
        }

        public float GetDifficult(float playTime)
        {
            float value = playTime * DifficultPerSecond;
            return Mathf.Clamp(value, IDifficultService.MinDifficultValue, MaxDifficultValue);
        }
    }
}