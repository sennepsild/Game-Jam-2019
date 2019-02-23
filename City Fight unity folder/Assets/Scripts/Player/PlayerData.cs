using City;
using SinputSystems;
using Units;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Player data", order = 50)]
    public class PlayerData : ScriptableObject
    {
        public int PlayerIndex;
        public InputDeviceSlot InputDeviceSlot;
        public float PopulationScore;
        public float FoodScore;
        public float WealthScore;
        public float PowerScore;
        
        public bool HasPriority { get; set; }
        public bool HasChosenCard { get; set; }
        public UnitManager UnitManager { get; private set; }
        public BuildPlacementManager BuildPlacementManager { get; set; }

        public void Init()
        {
            UnitManager = new UnitManager(this);
            new UnitBuildingManager(UnitManager, BuildPlacementManager);
        }

        public void Reset()
        {
            HasPriority = false;
            HasChosenCard = false;
        }
    }
}