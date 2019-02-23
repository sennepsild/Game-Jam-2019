using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUiManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _populationScoreText, _foodScoreText,_wealthScoreText, _powerScoreText;
    
    [SerializeField]
    private Bar _populationBar, _foodBar, _wealthBar, _powerBar;

    private PlayerData _playerData;

    public void Init(PlayerData playerData)
    {
        _playerData = playerData;
    }

    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        _populationScoreText.text = _playerData.PopulationScore.ToString();
        _foodScoreText.text = _playerData.FoodScore.ToString();
        _wealthScoreText.text = _playerData.WealthScore.ToString();
        _powerScoreText.text = _playerData.PowerScore.ToString();


        _populationBar.value = _playerData.PopulationScore;
        _foodBar.value = _playerData.FoodScore;
        _wealthBar.value = _playerData.WealthScore;
        _powerBar.value = _playerData.PowerScore;

        Bar.UpdateMax();
        _populationBar.resize();
        _foodBar.resize();
        _wealthBar.resize();
        _powerBar.resize();

    }
}
