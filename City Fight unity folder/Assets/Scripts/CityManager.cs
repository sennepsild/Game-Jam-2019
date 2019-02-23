using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityManager : MonoBehaviour
{

    public int playernumber;

    public Text PopulationScoreText,FoodScoreText,WealthScoreText,PowerScoreText;
    public float PopulationScore, FoodScore, WealthScore, PowerScore;
    public Resizer PopulationBar, FoodBar, WealthBar, PowerBar;



    List<Unit> Inhabitants = new List<Unit>();
    List<Building> Buildings = new List<Building>();

    private void Awake()
    {
        Resizer.players[playernumber] = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        AddInhabitant(new Unit());
        FoodScore = 5;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        PopulationScoreText.text = PopulationScore + "";
        FoodScoreText.text = FoodScore + "";
        WealthScoreText.text = WealthScore + "";
        PowerScoreText.text = PowerScore + "";


        PopulationBar.value = PopulationScore;
        FoodBar.value = FoodScore;
        WealthBar.value = WealthScore;
        PowerBar.value = PowerScore;

        Resizer.UpdateMax();
        PopulationBar.resize();
        FoodBar.resize();
        WealthBar.resize();
        PowerBar.resize();

    }



    public void AddInhabitant(Unit unit)
    {
        Inhabitants.Add(unit);
        PopulationScore += unit.Population;
        PowerScore += unit.Power();

        UpdateScore();

    }



    public void EndTurn()
    {
        foreach (Building b in Buildings)
        {
            b.Activate();
        }
    }


}
