using CardSystem;
using UnityEngine;

namespace DefaultNamespace
{
    public class TurnManager : MonoBehaviour
    {
        private const float TURN_DURATION = 5;
        
        [SerializeField]
        private CardUiManager _cardUiManager;

        private int _turnCount;

        private void Awake()
        {
            _cardUiManager.CardsHasBeenChosen += EndTurn;
        }

        private void EndTurn()
        {
            foreach (var playerDataEntry in Game.Instance.PlayerDataEntries)
            {
                playerDataEntry.UnitManager.OnTurn();
            }
            
            Timer.Register(TURN_DURATION, _cardUiManager.Show);
            _turnCount++;
        }
    }
}