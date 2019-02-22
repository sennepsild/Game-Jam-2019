using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit 
{
    public float Population = 1;
    public bool HasBetweenTurnEvent = false;
    public string UnitClass = "King";
    public string UnitRace = "Human";


    public Unit()
    {

    }


    public void SetupUnit()
    {

    }

    

    public virtual void specialEvent()
    {

    }


    public virtual void BetweenTurnEvent()
    {

    }


}
