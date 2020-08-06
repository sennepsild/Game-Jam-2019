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
        
        [SerializeField]
        private float _powerScore;
        
        [SerializeField]
        private float _foodScore;
        
        [SerializeField]
        private float _wealthScore;

        [SerializeField]
        private UnitData[] _unitsToStartWith;
        
        public float PopulationScore{ get; set; }

        public float FoodScore
        {
            get { return _foodScore; }
            set { _foodScore = value; }
        }

        public float WealthScore
        {
            get { return _wealthScore; }
            set { _wealthScore = value; }
        }

        public float PowerScore
        {
            get { return _powerScore; }
            set { _powerScore = value; }
        }

        public Sprite PrioritySprite;

        public bool HasPriority { get; set; }
        public bool HasChosenCard { get; set; }
        
        public bool IsAttacking { get; set; }
        
        public bool IsDefending { get; set; }
        public UnitManager UnitManager { get; private set; }
        public BuildPlacementManager BuildPlacementManager { get; set; }
       

        public void Init()
        {
            UnitManager = new UnitManager(this);
            new UnitBuildingManager(UnitManager, BuildPlacementManager);
            UnitManager.AddUnits(_unitsToStartWith);
        }

        public void Reset()
        {
            HasPriority = false;
            HasChosenCard = false;
            IsAttacking = false;
            IsDefending = false;
        }
    }
}