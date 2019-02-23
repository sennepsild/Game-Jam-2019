using System;
using System.Collections.Generic;
using DefaultNamespace;
using Extensions;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardSystem
{
    public class CardUiManager : MonoBehaviour
    {
        private const int CARDS_COUNT = 4;
        private const float SHOW_TIME_AFTER_PLAYERS_CHOSEN_CARDS = 3;
        private const float FADE_IN_DURATION = 1;
        private const float FADE_OUT_DURATION = 1;
        
        
        private List<CardInputData> _cardInputDataEntries = new List<CardInputData>
        {
            new CardInputData()
            {
                Inputkey = Controls.AButton,
                InputText = "A"
            },
            new CardInputData()
            {
                Inputkey = Controls.BButton,
                InputText = "B"
            },
            new CardInputData()
            {
                Inputkey = Controls.XButton,
                InputText = "X"
            },
            new CardInputData()
            {
                Inputkey = Controls.YButton,
                InputText = "Y"
            }
        };
        
        [SerializeField]
        private List<CardData> _cardDataEntries;

        [SerializeField]
        private CardUi _cardUiPrefab;

        [SerializeField]
        private Transform _cardsUiRoot;

        private List<CardUi> _spawnedCards;
        private bool _hasShownHighlights;

        public event Action CardsHasBeenChosen;

        private void Awake()
        {
            Show();
        }

        public void Show()
        {
            _hasShownHighlights = false;
            Game.Instance.ResetPlayerDataEntries();
            KillAllCards();
            List<CardData> randomCards = GetRandomCards();
            SpawnCards(randomCards);
        }

        public void Hide()
        {
            KillAllCards(FADE_OUT_DURATION);
            Timer.Register(FADE_OUT_DURATION, () => CardsHasBeenChosen.SafeInvoke());
        }

        private void KillAllCards(float fadeOutDuration = 0)
        {
            if (_spawnedCards != null)
            {
                foreach (CardUi cardUi in _spawnedCards)
                {
                    cardUi.Kill(fadeOutDuration);
                }
            }
            _spawnedCards = new List<CardUi>();
        }

        private void SpawnCards(List<CardData> cardDataEntries)
        {
            for (int i = 0; i < cardDataEntries.Count; i++)
            {
                CardUi cardUi = Instantiate(_cardUiPrefab, _cardsUiRoot);
                cardUi.SetCardData(cardDataEntries[i], _cardInputDataEntries[i]);
                cardUi.Show(FADE_IN_DURATION);
                _spawnedCards.Add(cardUi);
            }
        }

        private List<CardData> GetRandomCards()
        {
            List<CardData> possibleCards = GetPossibleCards();
            List<CardData> randomCards = new List<CardData>();

            for (int i = 0; i < CARDS_COUNT; i++)
            {
                CardData randomCard = Instantiate(possibleCards[Random.Range(0, possibleCards.Count)]);
                randomCards.Add(randomCard);
                possibleCards.Remove(randomCard);
            }

            return randomCards;
        }

        private List<CardData> GetPossibleCards()
        {
            List<CardData> possibleCards = new List<CardData>();

            foreach (var cardData in _cardDataEntries)
            {
                possibleCards.Add(Instantiate(cardData));
            }

            return possibleCards;
        }

        private void Update()
        {
            if (HasAllPlayersChosen() && !_hasShownHighlights)
            {
                ShownHighlights();
                Timer.Register(SHOW_TIME_AFTER_PLAYERS_CHOSEN_CARDS, Hide);
                _hasShownHighlights = true;
                ApplyCards();
            }
        }

        private void ShownHighlights()
        {
            foreach (var spawnedCard in _spawnedCards)
            {
                spawnedCard.ActivateChosenHighlights();
            }
        }

        public void ApplyCards()
        {
            foreach (var spawnedCard in _spawnedCards)
            {
                spawnedCard.Apply();
            }
        }

        private bool HasAllPlayersChosen()
        {
            foreach (var playerDataEntry in Game.Instance.PlayerDataEntries)
            {
                if (!playerDataEntry.HasChosenCard)
                {
                    return false;
                }
            }

            return true;
        }
    }
}