using DefaultNamespace;
using UnityEngine;

namespace Player
{
    public class PlayerSpawnManager : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _spawnPlaces;

        [SerializeField]
        private Transform _playerParent;

        [SerializeField]
        private Player _playerPrefab;
        
        private void Awake()
        {
            int i = 0;
            foreach (var playerDataEntry in Game.Instance.PlayerDataEntries)
            {
                Player spawnedPlayer = Instantiate(_playerPrefab, _spawnPlaces[i].position, Quaternion.identity, _playerParent);
                spawnedPlayer.Init(playerDataEntry);
                i++;
            }
        }
    }
}