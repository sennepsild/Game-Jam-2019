using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Extensions;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace CardSystem
{
    public class CardUiManager : MonoBehaviour
    {
        private const int CARDS_COUNT = 4;
        private const float SHOW_TIME_AFTER_PLAYERS_CHOSEN_CARDS = 3;
        private const float FADE_IN_DURATION = 1;
        private const float FADE_OUT_DURATION = 1;
        
        [SerializeField]
        private List<EventTypeData> _eventTypeDatas;

        [SerializeField]
        private List<CardInputData> _cardInputDataEntries;

        [SerializeField]
        private CardUi _cardUiPrefab;

        [SerializeField]
        private Transform _cardsUiRoot;

        [SerializeField]
        private TextMeshProUGUI _eventTitle;
        
        [SerializeField]
        private TextMeshProUGUI _eventDescription;

        [SerializeField]
        private CanvasGroup _uiRoot;

        [SerializeField]
        private Image _priorityImage;

        [SerializeField]
        private EventTypeData _combatEvents;

        [SerializeField] 
        private GameObject _priorityWithCrownGo;

        private List<CardUi> _spawnedCards;
        private EventType _lastEventType;
        private bool _hasShownHighlights;
        private int _playerPriorityIndex;

        public event Action CardsHasBeenChosen;

        private void Awake()
        {
            _playerPriorityIndex = 0;
        }

        public void Show(bool combat = false)
        {
            _priorityWithCrownGo.SetActive(!combat);
            _uiRoot.Activate();
            _hasShownHighlights = false;
            Game.Instance.ResetPlayerDataEntries();
            Game.Instance.GetPlayerData(_playerPriorityIndex).HasPriority = !combat;
            KillAllCards();
            List<CardData> cardsFromEvent = GetCards(combat);
            SpawnCards(cardsFromEvent);
        }

        private List<CardData> GetCards(bool combat)
        {
            if (combat)
            {
                return GetCardsFromEvents(new List<EventTypeData> { _combatEvents});
            }
            return GetCardsFromEvents(_eventTypeDatas);
        }

        public void Hide()
        {
            _uiRoot.Deactivate();
            KillAllCards(FADE_OUT_DURATION);
            Timer.Register(FADE_OUT_DURATION, () => CardsHasBeenChosen.SafeInvoke());
            _playerPriorityIndex++;
            if (_playerPriorityIndex >= Game.Instance.PlayerDataEntries.Count)
            {
                _playerPriorityIndex = 0;
            }
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

        private List<CardData> GetCardsFromEvents(List<EventTypeData> events)
        {
            List<EventTypeData> possibleEventTypeDataEntries =
                events.FindAll(item => item.EventType != _lastEventType);

            EventTypeData eventTypeData =
                possibleEventTypeDataEntries[Random.Range(0, possibleEventTypeDataEntries.Count)];
            EventData randomEventData = eventTypeData.CardsInEvent[Random.Range(0, eventTypeData.CardsInEvent.Count)];

            _eventTitle.text = randomEventData.Name;
            _eventDescription.text = randomEventData.Description;
            _priorityImage.sprite = Game.Instance.GetPlayerData(_playerPriorityIndex).PrioritySprite;

            _lastEventType = eventTypeData.EventType;


            return randomEventData.EventCards;
        }

        private void Update()
        {
            if (HasAllPlayersChosen() && !_hasShownHighlights)
            {
                ShownHighlights();
                Timer.Register(SHOW_TIME_AFTER_PLAYERS_CHOSEN_CARDS, Hide);
                _hasShownHighlights = true;
                ApplyCards();
                Attack();
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
        
        public void Attack()
        {
            foreach (var spawnedCard in _spawnedCards)
            {
                spawnedCard.Attack();
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