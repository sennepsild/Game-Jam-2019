using UnityEngine;

namespace City
{
    public interface IBuilding
    {
        void SetBuildingSprite(Sprite buildingSprite);
        void SetBuildingSize(Vector2 size);
        void Destroy();
    }
}