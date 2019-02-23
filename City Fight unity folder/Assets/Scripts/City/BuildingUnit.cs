using Player;
using Units;
using UnityEngine;

namespace City
{
    public class BuildingUnit : Unit<BuildingUnitData, BuildingUnit>
    {
        public BuildingUnit(BuildingUnitData unitData, PlayerData playerData, UnitManager unitManager) : base(unitData, playerData, unitManager)
        {
            _playerData.BuildPlacementManager.AddNormalBuilding(_unitData.BuildingSprite);
        }

        protected override void OnTurn()
        {
            _unitManager.AddUnits(_unitData.UnitsToAdd);
        }
    }
}