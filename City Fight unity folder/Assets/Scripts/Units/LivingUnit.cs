using System;
using City;
using Player;
using UnityEngine;

namespace Units
{
    public class LivingUnit : Unit<LivingUnitData, LivingUnit>
    {
        private RaceUiBuildingManager _raceUiBuildingManager;
        
        public LivingUnit(LivingUnitData unitData, PlayerData playerData, UnitManager unitManager) : base(unitData, playerData, unitManager)
        {
            ApplyClassModifications();
        }

        private void ApplyClassModifications()
        {
            switch (_unitData.UnitClass)
            {
                case UnitClass.Worker:
                    _unitData.FoodCost *= -1;
                    break;
                case UnitClass.Noble:
                    _unitData.Income *= 1.5f;
                    break;
                case UnitClass.Soldier:
                    _unitData.Power *= 1.5f;
                    break;
            }
        }
        
        protected override void OnTurn()
        {
            Eat();
        }

        private void Eat()
        {
            if (CanEat())
            {
                _playerData.FoodScore -= _unitData.FoodCost;
            }
            else
            {
                Die();
            }
        }

        private bool CanEat()
        {
            return _playerData.FoodScore >= _unitData.FoodCost;
        }
        
        private void Die()
        {
            _playerData.PopulationScore -= _unitData.Population;
            _unitManager.RemoveUnit(this);
        }
    }
}