using System.Collections.Generic;
using City;
using Player;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "BuildingUnit", menuName = "BuildingUnit", order = 52)]
    public class BuildingUnitData : UnitData<BuildingUnit>
    {
        public Sprite BuildingSprite;
        public List<UnitData> UnitsToAdd;
        
        protected override BuildingUnit OnCreateUnit(PlayerData playerData, UnitManager unitManager)
        {
            return new BuildingUnit(this, playerData, unitManager);
        }
    }
}