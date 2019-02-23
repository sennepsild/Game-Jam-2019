using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizer : MonoBehaviour
{
    public static float max = 10;

    public float value = 5;

    Vector3 size;
    Vector3 postion;

    public static CityManager[] players ;

    // Start is called before the first frame update
    private void Awake()
    {

        if (players == null)
        {
            players = new CityManager[3];
        }

        size = transform.localScale;
        postion = transform.position;



        
    }
  
   
    public void resize()
    {
        
       Vector3 temp = size;

        temp.y = temp.y *(value/max);

        
        transform.localScale = temp;

        Vector3 temp2 = postion;
        temp2.y -= size.y/2- temp.y/2;
        
        transform.position = temp2;
       

    }

    public static void UpdateMax()
    {
        float newMax = 10;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].FoodScore > newMax)
                newMax = players[i].FoodScore;

            if (players[i].PowerScore > newMax)
                newMax = players[i].PowerScore;

            if (players[i].WealthScore > newMax)
                newMax = players[i].WealthScore;

            if (players[i].PopulationScore > newMax)
                newMax = players[i].PopulationScore;
        }


        print("satan");
        max = newMax;
    }

}
