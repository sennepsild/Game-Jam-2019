using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityManager : MonoBehaviour
{
    public Text PopulationScoreText,FoodScoreText,WealthScoreText,PowerScoreText;
    public float PopulationScore, FoodScore, WealthScore, PowerScore;

    List<Unit> Inhabitants = new List<Unit>();


    // Start is called before the first frame update
    void Start()
    {
        AddInhabitant(new Unit());
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateScore()
    {
        PopulationScoreText.text = PopulationScore + "";
        FoodScoreText.text = FoodScore + "";
        WealthScoreText.text = WealthScore + "";
        PowerScoreText.text = PowerScore + "";
    }



    public void AddInhabitant(Unit unit)
    {
        Inhabitants.Add(unit);
        PopulationScore += unit.Population;

    }
}
