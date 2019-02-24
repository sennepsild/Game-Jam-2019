using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private const float MIN_FILL_VALUE = 0.1f;
    private const float MAX_FILL_VALUE = 1f;
    private static float max = 10;

    public float value { get; set; }

    [SerializeField]
    private Image _image;
  
   
    public void resize()
    {
        _image.fillAmount = Mathf.Min(Mathf.Max(MIN_FILL_VALUE, value / max), MAX_FILL_VALUE);
    }

    public static void UpdateMax()
    {
        float newMax = 10;
        for (int i = 0; i < Game.Instance.PlayerDataEntries.Count; i++)
        {
            PlayerData playerData = Game.Instance.GetPlayerData(i);
            
            if (playerData.FoodScore > newMax)
                newMax = playerData.FoodScore;

            if (playerData.PowerScore > newMax)
                newMax = playerData.PowerScore;

            if (playerData.WealthScore > newMax)
                newMax = playerData.WealthScore;

            if (playerData.PopulationScore > newMax)
                newMax = playerData.PopulationScore;
        }
        
        max = newMax;
    }

}
