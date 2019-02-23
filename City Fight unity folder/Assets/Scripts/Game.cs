using System.Collections.Generic;
using Player;
using UnityEngine;

namespace DefaultNamespace
{
    public class Game : MonoBehaviour
    {
        private static Game _instance;

        public static Game Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Game>();
                }

                return _instance;
            }
        }

        [SerializeField]
        private PlayerData[] _playerDataTemplates;

        private List<PlayerData> _playerDataEntries;

        public List<PlayerData> PlayerDataEntries
        {
            get
            {
                if (_playerDataEntries == null)
                {
                    SpawnPlayerDataEntries();
                }

                return _playerDataEntries;
            }
        }
        
        private void Awake()
        {
            if (FindObjectOfType<Game>() != Instance)
            {
                Destroy(this);
            }
            DontDestroyOnLoad(gameObject);
        }

        private void SpawnPlayerDataEntries()
        {
            _playerDataEntries = new List<PlayerData>();
            foreach (var playerDataTemplate in _playerDataTemplates)
            {
                _playerDataEntries.Add(Instantiate(playerDataTemplate));
            }   
        }

        public PlayerData GetPlayerData(int playerIndex)
        {
            return PlayerDataEntries[playerIndex];
        }

        public void ResetPlayerDataEntries()
        {
            foreach (var playerDataEntry in _playerDataEntries)
            {
                playerDataEntry.Reset();
            }
        }
    }
}