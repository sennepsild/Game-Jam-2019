using Player;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "Living unit data", menuName = "Living unit data", order = 51)]
    public class LivingUnitData : UnitData<LivingUnit>
    {
        public float FoodCost;
        public float ChanceOfDyingInCombat;
        public UnitClass UnitClass;
        public Sprite BuildingSprite;
        
        protected override LivingUnit OnCreateUnit(PlayerData playerData, UnitManager unitManager)
        {
            return new LivingUnit(this, playerData, unitManager);
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode() + (int)(FoodCost + (int)UnitClass * 10);
        }
    }
}