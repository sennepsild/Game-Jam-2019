using System;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace CardSystem
{
    public class CardChosenHighlightUi : MonoBehaviour
    {
        private float FADE_TIME = 1;
        
        [SerializeField]
        private CanvasGroup[] _chosenHighlights;

        private void Awake()
        {
            foreach (var chosenHighlight in _chosenHighlights)
            {
                chosenHighlight.gameObject.SetActive(false);
            }
        }

        public void Activate(IEnumerable<PlayerData> playerDataEntries)
        {
            foreach (var playerDataEntry in playerDataEntries)
            {
                _chosenHighlights[playerDataEntry.PlayerIndex].gameObject.SetActive(true);
                _chosenHighlights[playerDataEntry.PlayerIndex].DOFade(1, FADE_TIME);
            }
        }
        
        public void Deactivate(IEnumerable<PlayerData> playerDataEntries)
        {
            foreach (var playerDataEntry in playerDataEntries)
            {
                _chosenHighlights[playerDataEntry.PlayerIndex].gameObject.SetActive(false);
                _chosenHighlights[playerDataEntry.PlayerIndex].DOFade(0, FADE_TIME);
            }
        }
    }
}