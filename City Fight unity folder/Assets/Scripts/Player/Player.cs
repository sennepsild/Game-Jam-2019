using City;
using DefaultNamespace;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private BuildPlacementManager _buildPlacementManager;

        [SerializeField]
        private PlayerUiManager _playerUiManager;

        public void Init(PlayerData playerData)
        {
            playerData.BuildPlacementManager = _buildPlacementManager;
            playerData.Init();
            _playerUiManager.Init(playerData);
        }
    }
}