using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace City
{
    public class BuildPlacementManager : MonoBehaviour
    {
        private const float DEGREES_PER_BUILDING = 40;
        private const float DISTANCE_PER_CIRCLE = 1;

        [SerializeField]
        private Transform _center;

        [SerializeField]
        private Transform _buildingParent;

        [SerializeField]
        private BuildingUiManager _buildingUiManagerPrefab;
        
        [SerializeField]
        private RaceUiBuildingManager _raceBuildingUiManagerPrefab;

        private int _currentRing;
        private int _maxBuildingsPerCircle;
        private int _currentBuildCountInCircle;

        private List<IBuilding> _buildings;

        private void Awake()
        {
            _currentRing = 1;
            _currentBuildCountInCircle = 0;
            _maxBuildingsPerCircle = Mathf.FloorToInt(360 / DEGREES_PER_BUILDING);
            _buildings = new List<IBuilding>();
        }
        
        public IBuilding AddNormalBuilding(Sprite buildingSprite, Vector2 size)
        {
            return AddBuilding(_buildingUiManagerPrefab, buildingSprite, size);
        }

        public IBuilding AddRaceBuilding(Sprite buildingSprite)
        {
            return AddBuilding(_raceBuildingUiManagerPrefab, buildingSprite, Vector2.zero);
        }
        
        private IBuilding AddBuilding(Object obj, Sprite buildingSprite, Vector2 size)
        {
            IBuilding building = Instantiate(obj, GetBuildPosition(), Quaternion.identity, _buildingParent) as IBuilding;
            building.SetBuildingSprite(buildingSprite);
            if (size != Vector2.zero)
            {
                building.SetBuildingSize(size);
            }
            _buildings.Add(building);
            _currentBuildCountInCircle++;

            return building;
        }

        private Vector2 GetBuildPosition()
        {
            return _center.position +  Quaternion.AngleAxis(DEGREES_PER_BUILDING * _currentBuildCountInCircle, Vector2.up) * (Vector2.up * DISTANCE_PER_CIRCLE);
        }

        public void RemoveBuilding(IBuilding building)
        {
            building.Destroy();
            _buildings.Remove(building);
        }
    }
}