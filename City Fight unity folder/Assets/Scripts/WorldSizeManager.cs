using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class WorldSizeManager : MonoBehaviour
    {
        private static WorldSizeManager _instance;
        
        public static WorldSizeManager Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<WorldSizeManager>();
                }
    
                return _instance;
            }
        }
        
        [SerializeField]
        private CanvasScaler _canvasScaler;
        
        public Rect ToWorld(RectTransform rectTransform)
        {
            return rectTransform.GetWorldRect(_canvasScaler.scaleFactor * Vector2.one);
        }
    }
}