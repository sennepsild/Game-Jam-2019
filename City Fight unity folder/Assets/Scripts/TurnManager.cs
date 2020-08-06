using CardSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class TurnManager : MonoBehaviour
    {
        private const float TURN_DURATION = 5;
        private const int COMBAT_ROUND_TURN_COUNT = 5;
        private const int COUNT_OF_ROUNDS = 25;
        
        [SerializeField]
        private CardUiManager _cardUiManager;

        private int _currentTurnCount;

        private void Awake()
        {
            _cardUiManager.CardsHasBeenChosen += EndTurn;
            _currentTurnCount = 1;
            OnEndOfTurn();
        }

        private void EndTurn()
        {
            foreach (var playerDataEntry in Game.Instance.PlayerDataEntries)
            {
                playerDataEntry.UnitManager.OnTurn();
            }
            
            Timer.Register(TURN_DURATION, OnEndOfTurn);
            _currentTurnCount++;
        }

        private void OnEndOfTurn()
        {
            bool startingCombat = _currentTurnCount % COMBAT_ROUND_TURN_COUNT == 0;
            _cardUiManager.Show(startingCombat);
            if (_currentTurnCount > COUNT_OF_ROUNDS)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}