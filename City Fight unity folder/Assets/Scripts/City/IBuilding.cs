using UnityEngine;

namespace City
{
    public interface IBuilding
    {
        void SetBuildingSprite(Sprite buildingSprite);
        void Destroy();
    }
}