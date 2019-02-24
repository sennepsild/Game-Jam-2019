using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Units
{
    public class UnitManager
    {
        private PlayerData _playerData;

        public List<IUnit> Units { get; private set; }

        public event Action<UnitData> UnitAdded;
        
        public UnitManager(PlayerData playerData)
        {
            Units = new List<IUnit>();
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
            Units.Add(unit);
        }

        public void OnTurn()
        {
            foreach (var unit in Units)
            {
                unit.Turn();
            }
        }

        public void RemoveUnits(IEnumerable<IUnit> units)
        {
            foreach (var unit in units)
            {
                RemoveUnit(unit);
            }
        }
        
        public void RemoveUnit(IUnit unit)
        {
            Units.Remove(unit);
        }

        public IUnit GetRandomUnit()
        {
            return Units[Random.Range(0, Units.Count)];
        }
    }
}