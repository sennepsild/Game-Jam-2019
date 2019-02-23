using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using Extensions;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardSystem
{
    public class CardUi : MonoBehaviour
    {
        private const string INPUT_DESCRIPTION = "Press {0}";
        private const float MINIMUM_CARD_CHOSEN_HIGHLIGHT_DURATION = 2;
        
        [SerializeField]
        private TextMeshProUGUI _titleLabel;
        
        [SerializeField]
        private TextMeshProUGUI _descriptionLabel;

        [SerializeField] 
        private Image _cardImage;

        [SerializeField] 
        private TextMeshProUGUI _inputDescription;

        [SerializeField]
        private CardChosenHighlightUi _cardChosenHighlightUi;

        [SerializeField] 
        private CanvasGroup _root;

        private CardInputData _cardInputData;
        private List<PlayerData> _playersChosenThisCard;

        public CardData CardData { get; private set; }

        public void SetCardData(CardData cardData, CardInputData cardInputData)
        { 
            CardData = cardData;
            _cardInputData = cardInputData;
            _titleLabel.text = cardData.Title;
            _descriptionLabel.text = cardData.Description;
            _cardImage.sprite = cardData.CardSprite;
            _inputDescription.text = string.Format(INPUT_DESCRIPTION, cardInputData.InputText);
        }

        public void Show(float fadeDuration)
        {
            _root.Deactivate();
            _playersChosenThisCard = new List<PlayerData>();
            PlayerInputManager.Instance.ButtonPressed += OnButtonPressed;
            _root.DOFade(1, fadeDuration);
        }

        private void OnButtonPressed(string inputNamePressed, PlayerData playerData)
        {
            if (inputNamePressed == _cardInputData.Inputkey && !playerData.HasChosenCard)
            {
                playerData.HasChosenCard = true;
                _playersChosenThisCard.Add(playerData);
            }
        }

        public void ActivateChosenHighlights()
        {
            _cardChosenHighlightUi.Activate(_playersChosenThisCard);
            PlayerData playerWithPriority = _playersChosenThisCard.Find(item => item.HasPriority);
            if (playerWithPriority != null)
            {
                _playersChosenThisCard.Remove(playerWithPriority);
                Timer.Register(MINIMUM_CARD_CHOSEN_HIGHLIGHT_DURATION,
                    () => _cardChosenHighlightUi.Deactivate(_playersChosenThisCard));
            }
        }

        public void Apply()
        {
            PlayerData priorityPlayer = _playersChosenThisCard.Find(item => item.HasPriority);
            if (priorityPlayer != null)
            {
                CardData.ApplyCardTo(priorityPlayer, _playersChosenThisCard.Count > 1);
            }
            else
            {
                ApplyCardToAllPlayers();
            }
        }

        private void ApplyCardToAllPlayers()
        {
            foreach (var playerData in _playersChosenThisCard)
            {
                CardData.ApplyCardTo(playerData, _playersChosenThisCard.Count > 1);
            }
        }

        public void Kill(float killFadeDuration)
        {
            _root.DOFade(0, killFadeDuration);
            Timer.Register(killFadeDuration, () => Destroy(gameObject));
        }

        private void OnDestroy()
        {
            if (PlayerInputManager.Instance)
            {
                PlayerInputManager.Instance.ButtonPressed -= OnButtonPressed;
            }
        }
    }
}