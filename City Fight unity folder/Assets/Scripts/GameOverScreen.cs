using System.Linq;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameOverScreen : MonoBehaviour
    {
        
        private const string WIN_TEXT = "Player {0} has won with {1} population. Restarting in 30 seconds.";
        
        [SerializeField]
        private TextMeshProUGUI _gameOverLabel;

        private void Awake()
        {
            float scoreOfPlayerWhoWon = Game.Instance.PlayerDataEntries.Max(item => item.PopulationScore);

            PlayerData playerWhoWon =
                Game.Instance.PlayerDataEntries.Find(item => item.PopulationScore == scoreOfPlayerWhoWon);

            _gameOverLabel.text = string.Format(WIN_TEXT, playerWhoWon.PlayerIndex, playerWhoWon.PopulationScore);

            Timer.Register(30, () => SceneManager.LoadScene("MainScene"));
        }
    }
}