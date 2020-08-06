using System;
using System.Collections.Generic;
using Banagine.EC;
using Player;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Units
{
    public class UnitManager
    {
        private PlayerData _playerData;

        public DelayedList<IUnit> Units { get; private set; }

        public event Action<UnitData> UnitAdded;
        
        public UnitManager(PlayerData playerData)
        {
            Units = new DelayedList<IUnit>();
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
            UnitData unitDataClose = Object.Instantiate(unitData);
            if (UnitAdded != null)
            {
                UnitAdded.Invoke(unitDataClose);
            }
            IUnit unit = unitDataClose.CreateUnit(_playerData, this);
            Units.Add(unit);
        }

        public void OnTurn()
        {
            foreach (var unit in Units)
            {
                unit.Turn();
            }
            
            Units.UpdateList();
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
            if (Units.Count <= 0)
            {
                Debug.LogError("Tried to get random unit, but there is no units left");
                return null;
            }
            
            return Units.GetItemAtIndex(Random.Range(0, Units.Count));
        }
    }
}