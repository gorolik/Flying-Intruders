using UnityEngine;

namespace Sources.StaticData.Hole
{
    [CreateAssetMenu(fileName = "HoleData", menuName = "Static Data/Hole Data")]
    public class HoleData : ScriptableObject
    {
        [SerializeField] private float _health = 10;
        
        public float Health => _health;
    }
}