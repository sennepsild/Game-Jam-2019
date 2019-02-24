using City;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private BuildPlacementManager _buildPlacementManager;

        [SerializeField]
        private Image _flagImage;

        [SerializeField]
        private Sprite[] _flagSprites;

        public void Init(PlayerData playerData)
        {
            _flagImage.sprite = _flagSprites[playerData.PlayerIndex];
            playerData.BuildPlacementManager = _buildPlacementManager;
            playerData.Init();
        }
    }
}