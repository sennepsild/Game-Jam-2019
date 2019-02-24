using System.Collections.Generic;
using City;
using Player;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "BuildingUnit", menuName = "BuildingUnit", order = 52)]
    public class BuildingUnitData : UnitData<BuildingUnit>
    {
        public float Food;

        public Sprite BuildingSprite;
        public Vector2 Size;


        public List<UnitData> UnitsToAdd;
        
        protected override BuildingUnit OnCreateUnit(PlayerData playerData, UnitManager unitManager)
        {
            return new BuildingUnit(this, playerData, unitManager);
        }
        
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode() + (int)(Food *   10);

            foreach (var unitData in UnitsToAdd)
            {
                hashCode += unitData.GetHashCode();
            }

            return hashCode;
        }
    }
}