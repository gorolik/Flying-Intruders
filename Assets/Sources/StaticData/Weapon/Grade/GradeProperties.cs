using System;
using UnityEngine;

namespace Sources.StaticData.Weapon.Grade
{
    [Serializable]
    public struct GradeProperties
    {
        [Range(0.01f, 1)] public float CooldownGradePercent;
        [Range(0.01f, 0.1f)] public float SpreadGradePercent;
        [Range(0.01f, 1f)] public float ProjectileSpeedGradePercent;
        [Range(0.01f, 1f)] public float ProjectileDamageGradePercent;
    }
}