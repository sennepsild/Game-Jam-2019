using CardSystem;
using UnityEngine;

namespace DefaultNamespace
{
    public class TurnManager : MonoBehaviour
    {
        private const float TURN_DURATION = 5;
        private const int COMBAT_ROUND_TURN_COUNT = 5;
        
        [SerializeField]
        private CardUiManager _cardUiManager;

        private int _turnCount;

        private void Awake()
        {
            _cardUiManager.CardsHasBeenChosen += EndTurn;
            _turnCount = 1;
            OnEndOfTurn();
        }

        private void EndTurn()
        {
            foreach (var playerDataEntry in Game.Instance.PlayerDataEntries)
            {
                playerDataEntry.UnitManager.OnTurn();
            }
            
            Timer.Register(TURN_DURATION, OnEndOfTurn);
            _turnCount++;
        }

        private void OnEndOfTurn()
        {
            bool startingCombat = _turnCount % COMBAT_ROUND_TURN_COUNT == 0;
            _cardUiManager.Show(startingCombat);
        }
    }
}