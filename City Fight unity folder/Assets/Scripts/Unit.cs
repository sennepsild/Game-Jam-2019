using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit 
{
    public float Population = 1;
    public float Income = 1;
    public float foodCost = 1;
    float power = 0;




    public bool HasBetweenTurnEvent = false;
    public string UnitClass = "King";
    public string UnitRace = "Human";


  

    public Unit(string UnitClass, string UnitRace)
    {
        this.UnitClass = UnitClass;
        this.UnitRace = UnitRace;
        SetupUnit();

    }
    
    public Unit()
    {
       
        SetupUnit();

    }


    public void SetupUnit()
    {

    }

    

    public virtual void specialEvent()
    {

    }
    public float Power()
    {

        return power;
    }


}
