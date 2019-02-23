using System.Collections.Generic;
using Player;
using Units;
using UnityEngine;

namespace CardSystem
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Card data", order = 50)]
    public class CardData : ScriptableObject
    {
        public string Title;
        public string Description;
        public Sprite CardSprite;
        public List<UnitData> UnitsToAdd;
        [Header("Change")]
        public int WealthChange;

        public int PowerChange;
        public int FoodChange;
        [Header("Cost")] 
        public int WealthCost;
        public int PowerCost;
        public int FoodCost;


        public void ApplyCardTo(PlayerData playerData, bool split)
        {
            playerData.FoodScore += GetFoodChangeAmount(split);
            playerData.WealthScore += GetWealthChangeAmount(split);
            playerData.PowerScore += GetPowerChangeAmount(split);
            playerData.UnitManager.AddUnits(GetUnitsToAddAmount(split));
        }

        private int GetFoodChangeAmount(bool split)
        {
            int foodAmount = (FoodChange + FoodCost);
            
            if (split)
            {
                return foodAmount / 2;
            }

            return foodAmount;
        }
        
        private int GetWealthChangeAmount(bool split)
        {
            int foodAmount = (WealthChange + WealthCost);
            
            if (split)
            {
                return foodAmount / 2;
            }

            return foodAmount;
        }
        
        private int GetPowerChangeAmount(bool split)
        {
            int foodAmount = (PowerChange + PowerCost);
            
            if (split)
            {
                return foodAmount / 2;
            }

            return foodAmount;
        }
        
        private IEnumerable<UnitData> GetUnitsToAddAmount(bool split)
        {
            int unitCount = UnitsToAdd.Count;
            if (split)
            {
                unitCount /= 2;
            }

            for (int i = 0; i < unitCount; i++)
            {
                yield return UnitsToAdd[i];
            }
        }
    }
}