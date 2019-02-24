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

    [SerializeField]
    private int _playerIndex;

    private PlayerData _playerData;

    private void Awake()
    {
        _playerData = Game.Instance.GetPlayerData(_playerIndex);
    }

    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        _populationScoreText.text = _playerData.PopulationScore.ToString("F0");
        _foodScoreText.text = _playerData.FoodScore.ToString("F0");
        _wealthScoreText.text = _playerData.WealthScore.ToString("F0");
        _powerScoreText.text = _playerData.PowerScore.ToString("F0");


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
