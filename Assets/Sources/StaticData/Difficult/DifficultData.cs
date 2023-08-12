using UnityEngine;

namespace Sources.StaticData.Difficult
{
    [CreateAssetMenu(fileName = "DifficultData", menuName = "Static Data/Difficult Data")]
    public class DifficultData : ScriptableObject
    {
        [SerializeField] private float _difficultPerSecond;
        public float DifficultPerSecond => _difficultPerSecond;
    }
}