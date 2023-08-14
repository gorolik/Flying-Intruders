using UnityEngine;

namespace Sources.StaticData.Weapon.Grade
{
    [CreateAssetMenu(fileName = "GradeData", menuName = "Static Data/Grade Data")]
    public class GradeData : ScriptableObject
    {
        [SerializeField] private GradeProperties _gradeProperties;

        public GradeProperties GradeProperties => _gradeProperties;
    }
}