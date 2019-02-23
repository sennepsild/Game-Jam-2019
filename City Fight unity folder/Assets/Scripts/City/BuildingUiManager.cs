using UnityEngine;
using UnityEngine.UI;

namespace City
{
    public class BuildingUiManager : MonoBehaviour, IBuilding
    {
        [SerializeField]
        private Image _image;

        public void SetBuildingSprite(Sprite buildingSprite)
        {
            _image.sprite = buildingSprite;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}