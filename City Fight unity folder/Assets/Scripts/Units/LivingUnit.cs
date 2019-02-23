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