using System.Collections.Generic;
using DefaultNamespace;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace City
{
    public class RaceUiBuildingManager : MonoBehaviour, IBuilding
    {
        [SerializeField]
        private Transform _leftUpperCorner;

        [SerializeField]
        private Image _raceBuildingPrefab;

        [SerializeField]
        private Image _image;

        [SerializeField]
        private Transform _buildingsParent;

        private Vector2 _lastSpawnPosition;
        private bool[,] _grid;
        private Sprite _buildingSprite;
        private Vector2 _gridSize;

        private void Awake()
        {
            _gridSize = _image.rectTransform.rect.size / _raceBuildingPrefab.rectTransform.rect.size;
            _grid = new bool[(int)_gridSize.x, (int)_gridSize.y];
        }

        public void SetBuildingSprite(Sprite buildingSprite)
        {
            _buildingSprite = buildingSprite;
            SpawnBuilding();
        }

        public void SpawnBuilding()
        {
            Vector2 spawnPosition = GetSpawnPosition();
            Image building = Instantiate(_raceBuildingPrefab, GetSpawnPosition(), Quaternion.identity, _buildingsParent);
            building.rectTransform.localPosition = spawnPosition;
            building.sprite = _buildingSprite;
        }

        public bool HasReachedLimit()
        {
            return GetPossibleSpawnPositions().Count <= 0;
        }
        
        public void Destroy()
        {
            Destroy(gameObject);
        }

        private Vector2 GetSpawnPosition()
        {
            List<Vector2> possibleSpawnPosition = GetPossibleSpawnPositions();
            Vector2 randomSpawnPosition = possibleSpawnPosition[Random.Range(0, possibleSpawnPosition.Count)];
            _grid[(int)randomSpawnPosition.x, (int)randomSpawnPosition.y] = true;
            return (Vector2)_leftUpperCorner.localPosition + ToWorldPosition(randomSpawnPosition * Vector2.down);
        }

        private List<Vector2> GetPossibleSpawnPositions()
        {
            List<Vector2> possibleSpawnPosition = new List<Vector2>();
            
            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {
                    if (!_grid[x, y])
                    {
                        possibleSpawnPosition.Add(new Vector2(x, y));
                    }
                }
            }

            return possibleSpawnPosition;
        }

        private Vector2 ToWorldPosition(Vector2 gridPosition)
        {
            return gridPosition * _raceBuildingPrefab.rectTransform.rect.size;
        }
    }
}