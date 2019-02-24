using Player;
using Units;
using UnityEngine;

namespace City
{
    public class BuildingUnit : Unit<BuildingUnitData, BuildingUnit>
    {
        public BuildingUnit(BuildingUnitData unitData, PlayerData playerData, UnitManager unitManager) : base(unitData, playerData, unitManager)
        {
            _playerData.BuildPlacementManager.AddNormalBuilding(_unitData.BuildingSprite, _unitData.Size);
        }

        protected override void OnTurn()
        {
            _playerData.FoodScore += _unitData.Food;
            _playerData.WealthScore += _unitData.Income;
            _playerData.PopulationScore += _unitData.Population;
            _unitManager.AddUnits(_unitData.UnitsToAdd);
        }
    }
}