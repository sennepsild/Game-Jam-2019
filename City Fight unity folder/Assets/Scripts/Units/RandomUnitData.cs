using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "RandomUnitData", menuName = "Random unit data", order = 51)]
    public class RandomUnitData : UnitData<LivingUnit>
    {
        public List<LivingUnitData> LivingUnitDataEntries;
        
        protected override LivingUnit OnCreateUnit(PlayerData playerData, UnitManager unitManager)
        {
            return new LivingUnit(LivingUnitDataEntries[Random.Range(0, LivingUnitDataEntries.Count)], playerData, unitManager);
        }
    }
}