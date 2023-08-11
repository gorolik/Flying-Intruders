using System.Collections.Generic;
using System.Linq;
using Sources.StaticData.Enemy;
using Sources.StaticData.Hole;
using UnityEngine;

namespace Sources.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private HoleData _holeData;
        private Dictionary<EnemyType, EnemyData> _enemys;

        public void LoadHoleData() => 
            _holeData = Resources.Load<HoleData>("StaticData/Hole/Hole");

        public HoleData GetHoleData() => 
            _holeData;

        public void LoadEnemysData()
        {
            _enemys = Resources
                .LoadAll<EnemyData>("StaticData/Enemys")
                .ToDictionary(x => x.Type, x => x);
        }

        public EnemyData GetEnemyDataByType(EnemyType type) => 
            _enemys.TryGetValue(type, out EnemyData data) ? data : null;
    }
}