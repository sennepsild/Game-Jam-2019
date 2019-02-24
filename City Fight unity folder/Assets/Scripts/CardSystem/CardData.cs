using System;
using System.Collections.Generic;
using DefaultNamespace;
using Player;
using Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardSystem
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Card data", order = 50)]
    public class CardData : ScriptableObject
    {
        private const float MIN_ATTACK_PROCENT = 0.20f;
        private const float MAX_ATTACK_PROCENT = 0.80f;
        
        public string Title;
        public string Description;
        public List<UnitData> UnitsToAdd;
        public bool EvenSplit;
        [Header("Change")]
        public int WealthChange;
        public int PowerChange;
        public int FoodChange;
        [Header("Cost")] 
        public int WealthCost;
        public int PowerCost;
        public int FoodCost;
        public int KillCost;
        [Header("Target")] 
        public TargetData TargetData;

        [Header("Combat")] 
        public int PlayerAttackIndex = -1;

        public void ApplyCardTo(PlayerData playerData, bool split)
        {
            if (CanAfford(playerData))
            {
                ApplyCosts(playerData);
                ApplyStats(playerData, split);
                ApplyTarget(playerData);
                ApplyAttack(playerData);
            }
        }
        
        private bool CanAfford(PlayerData playerData)
        {
            return playerData.FoodScore >= FoodCost && playerData.WealthScore >= WealthCost
                                                    && playerData.PowerScore >= PowerCost
                                                    && playerData.UnitManager.Units.Count >= KillCost;

        }

        private void ApplyStats(PlayerData playerData, bool split)
        {
            playerData.FoodScore += GetFoodChangeAmount(split);
            playerData.WealthScore += GetWealthChangeAmount(split);
            playerData.PowerScore += GetPowerChangeAmount(split);
            playerData.UnitManager.AddUnits(GetUnitsToAddAmount(split));
            
        }

        private void PayKillCost(PlayerData playerData)
        {
            for (int i = 0; i < KillCost; i++)
            {
                playerData.UnitManager.Units.Remove(playerData.UnitManager.GetRandomUnit());
            }
        }
        
        private void ApplyCosts(PlayerData playerData)
        {
            playerData.FoodScore -= FoodCost;
            playerData.WealthScore -= WealthCost;
            playerData.PowerScore -= PowerCost;
            PayKillCost(playerData);
        }

        private void ApplyTarget(PlayerData playerData)
        {
            if (TargetData.AmountToLose > 0)
            {
                DoDamageToTarget(playerData, TargetData.TargetType);
            }
        }

        private void ApplyAttack(PlayerData playerData)
        {
            if (PlayerAttackIndex > -1 && PlayerAttackIndex != playerData.PlayerIndex)
            {
                playerData.IsAttacking = true;
            }
            else if (PlayerAttackIndex > -1 && PlayerAttackIndex == playerData.PlayerIndex)
            {
                playerData.PowerTempBoost = playerData.PowerScore * 0.5f;
            }
        }

        public void Attack(PlayerData playerData)
        {
            if (!playerData.IsAttacking)
            {
                return;
            }
            
            PlayerData playerToAttack = Game.Instance.GetPlayerData(PlayerAttackIndex);
  
            float attackProcent = CalculateAttackProcent(playerData, playerToAttack);
            if (playerData.PowerScore > playerToAttack.PowerScore)
            {
                StealResources(attackProcent, playerData, playerToAttack);
            }
            Attack(attackProcent, playerData, playerToAttack);

        }

        private float CalculateAttackProcent(PlayerData playerData, PlayerData playerToAttack)
        {
            float playerAttackingPowerScore = playerToAttack.PowerScore;
            if (playerToAttack.IsAttacking)
            {
                playerAttackingPowerScore *= 0.5f;
            }
            float attackRatio = playerData.PowerScore / playerAttackingPowerScore;

            float unclampedAttackProcent = attackRatio - 1;
            
            Debug.Log("AttackRatio: " + attackRatio + " unclampedAttackProcent: " + unclampedAttackProcent);

            return Mathf.Min(Mathf.Max(unclampedAttackProcent, MIN_ATTACK_PROCENT), MAX_ATTACK_PROCENT);
        }

        private void Attack(float attackProcent, PlayerData playerData, PlayerData playerToAttack)
        {
            int playerUnitCount = playerToAttack.UnitManager.Units.Count;
            int playerUnitsToAttackCount = (int)(playerUnitCount * attackProcent);
            int unitsKilled = 0;

            for (int i = 0; i < playerUnitsToAttackCount; i++)
            {
                if (playerToAttack.UnitManager.Units.Count <= 0)
                {
                    break;
                }
                
                IUnit randomUnit =
                    playerToAttack.UnitManager.GetRandomUnit();
                int attackChance = Random.Range(0, 100);
                if (randomUnit.GetUnitData<LivingUnitData>().ChanceOfDyingInCombat >= (attackChance))
                {
                    playerToAttack.UnitManager.RemoveUnit(randomUnit);
                    unitsKilled++;
                }
            }
            
            Debug.Log("Player: " +  playerData.PlayerIndex + " killed: " + unitsKilled + " of " + playerToAttack.PlayerIndex);
        }

        private void StealResources(float attackProcent, PlayerData playerData, PlayerData playerToAttack)
        {
            Debug.Log("Player: " +  playerData.PlayerIndex + "Stole: " + playerToAttack.FoodScore * attackProcent +  " food from " + playerToAttack.PlayerIndex);
            Debug.Log("Player: " +  playerData.PlayerIndex + "Stole: " + playerToAttack.WealthScore * attackProcent +  " wealth from " + playerToAttack.PlayerIndex);
            playerData.FoodScore += playerToAttack.FoodScore * attackProcent;
            playerData.WealthScore += playerToAttack.WealthScore * attackProcent;
        }

        private void DoDamageToTarget(PlayerData playerData, TargetType targetType)
        {
            switch (targetType)
            {
                case TargetType.Specific:
                    List<IUnit> unitsToTarget = playerData.UnitManager.Units.FindAll(item =>
                        TargetData.SpecficUnitDataEntries.Contains(item.GetUnitData<UnitData>()));
                    playerData.UnitManager.RemoveUnits(unitsToTarget);
                    break;
                case TargetType.Random:
                    for (int i = 0; i < TargetData.AmountToLose; i++)
                    {
                        if (playerData.UnitManager.Units.Count <= 0)
                        {
                            break;
                        }
                        IUnit randomUnit =
                            playerData.UnitManager.GetRandomUnit();
                        playerData.UnitManager.RemoveUnit(randomUnit);
                    }
                    break;
                case TargetType.RandomSpecificRace:
                    UnitData randomUnitRace =
                        playerData.UnitManager.GetRandomUnit().GetUnitData<UnitData>();

                    for (int i = 0; i < TargetData.AmountToLose; i++)
                    {
                        IUnit unitWithRace = playerData.UnitManager.Units.Find(item => item.GetUnitData<UnitData>().Equals(randomUnitRace));
                        if (unitWithRace == null)
                        {
                            break;
                        }
                        
                        playerData.UnitManager.RemoveUnit(unitWithRace);
                    }
                    break;
            }
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
            if (split && !EvenSplit)
            {
                unitCount /= 2;
            }

            for (int i = 0; i < unitCount; i++)
            {
                yield return UnitsToAdd[i];
            }
        }
    }
    
    [Serializable]
    public class TargetData
    {
        public int AmountToLose;
        public TargetType TargetType;
        public UnitClass TargetClass;
        public List<UnitData> SpecficUnitDataEntries;
    }

    public enum TargetType
    {
        Specific,
        Random,
        RandomSpecificRace,
        
    }
}