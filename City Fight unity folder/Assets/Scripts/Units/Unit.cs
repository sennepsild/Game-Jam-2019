﻿using System;
using Player;

namespace Units
{
    public abstract class Unit<TUnitData, TUnit> : IUnit where TUnit : IUnit where TUnitData : UnitData<TUnit>
    {
        protected TUnitData _unitData;
        protected PlayerData _playerData;
        protected UnitManager _unitManager;
        
        public Unit(TUnitData unitData, PlayerData playerData, UnitManager unitManager)
        {
            _playerData = playerData;
            _unitData = unitData;
            _unitManager = unitManager;
            AddScores();
        }

        private void AddScores()
        {
            _playerData.PopulationScore += _unitData.Population;
        }

        public T GetUnitData<T>() where T : UnitData
        {
            return _unitData as T;
        }

        public void Turn()
        {
            IncreaseStats();
            OnTurn();
        }

        protected abstract void OnTurn();

        private void IncreaseStats()
        {
            _playerData.WealthScore += _unitData.Income;
        }
    }
}
