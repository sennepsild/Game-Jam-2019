using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Units
{
    public class UnitManager
    {
        private PlayerData _playerData;

        private List<IUnit> _units;

        public event Action<UnitData> UnitAdded;
        
        public UnitManager(PlayerData playerData)
        {
            _units = new List<IUnit>();
            _playerData = playerData;
        }

        public void AddUnits(IEnumerable<UnitData> unitDataEntries)
        {
            foreach (var unitDataEntry in unitDataEntries)
            {
                AddUnit(unitDataEntry);
            }
        }
        
        public void AddUnit(UnitData unitData)
        {
            if (UnitAdded != null)
            {
                UnitAdded.Invoke(unitData);
            }
            IUnit unit = unitData.CreateUnit(_playerData, this);
            _units.Add(unit);
        }

        public void OnTurn()
        {
            foreach (var unit in _units)
            {
                unit.Turn();
            }
        }

        public void RemoveUnit(IUnit unit)
        {
            _units.Remove(unit);
        }
    }
}