using System;
using System.Collections.Generic;
using City;

namespace Units
{
    public class UnitBuildingManager
    {
        private const int UNIT_FOR_BUILDING = 5;
        
        private Dictionary<Type, RaceUiBuildingManager> _typeToRaceUiBuildingManager;
        private Dictionary<Type, int> _unitDataTypesCount;
        private UnitManager _unitManager;
        private BuildPlacementManager _buildPlacementManager;

        public UnitBuildingManager(UnitManager unitManager, BuildPlacementManager buildPlacementManager)
        {
            _typeToRaceUiBuildingManager = new Dictionary<Type, RaceUiBuildingManager>();
            _unitDataTypesCount = new Dictionary<Type, int>();
            _unitManager = unitManager;
            _buildPlacementManager = buildPlacementManager;
            _unitManager.UnitAdded += OnUnitAdded;
        }

        private void OnUnitAdded(UnitData unitData)
        {
            LivingUnitData livingUnitData = unitData as LivingUnitData;
            Type unitType = livingUnitData.GetType();
            if (livingUnitData != null)
            {
                if ((IsFirstTimePlacingBuildingForUnitType(unitType) || HasReachedMaxCountForUnitType(unitType)))
                {
                    SpawnBuilding(livingUnitData, unitType);
                }

                CountUnitTypes(unitType);
            }
        }

        private void CountUnitTypes(Type unitType)
        {
            if (!_unitDataTypesCount.ContainsKey(unitType))
            {
                _unitDataTypesCount[unitType] = 0;
            }
            else
            {
                _unitDataTypesCount[unitType]++;
            }
        }

        private void SpawnBuilding(LivingUnitData livingUnitData, Type unitType)
        {
            if (!_typeToRaceUiBuildingManager.ContainsKey(unitType) || _typeToRaceUiBuildingManager[unitType].HasReachedLimit())
            {
                _typeToRaceUiBuildingManager[unitType] = (RaceUiBuildingManager)_buildPlacementManager.AddRaceBuilding(livingUnitData.BuildingSprite);
            }
            else if (_typeToRaceUiBuildingManager.ContainsKey(unitType))
            {
                _typeToRaceUiBuildingManager[unitType].SpawnBuilding();
            }

            _unitDataTypesCount[unitType] = 0;
        }

        private bool IsFirstTimePlacingBuildingForUnitType(Type unitType)
        {
            return !_unitDataTypesCount.ContainsKey(unitType);
        }
        
        private bool HasReachedMaxCountForUnitType(Type unitType)
        {
            return  _unitDataTypesCount[unitType] >= UNIT_FOR_BUILDING;
        }
    }
}