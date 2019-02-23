using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Player
{
    public delegate void PlayerInputPressed(string inputNamePressed, PlayerData playerData);
    
    public class PlayerInputManager : MonoBehaviour
    {
        public event PlayerInputPressed ButtonPressed;

        private List<PlayerInput> _playerInputs;

        private static PlayerInputManager _instance;

        public static PlayerInputManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PlayerInputManager>();
                }

                return _instance;
            }
        }

        private void Awake()
        {
            AddPlayerInputs();
        }

        private void AddPlayerInputs()
        {
            _playerInputs = new List<PlayerInput>();
            foreach (var playerDataEntry in Game.Instance.PlayerDataEntries)
            {
                PlayerInput playerInput = new PlayerInput(playerDataEntry);
                _playerInputs.Add(playerInput);
                playerInput.InputPressed += (inputPressed) => InvokePlayerInputPressed(inputPressed, playerDataEntry);
            }
        }

        private void InvokePlayerInputPressed(string inputPressed, PlayerData playerData)
        {
            print("PlayerInputPressed: " + playerData.InputDeviceSlot);
            if (ButtonPressed != null)
            {
                ButtonPressed.Invoke(inputPressed, playerData);
            }
        }

        private void Update()
        {
            foreach (var playerInput in _playerInputs)
            {
                playerInput.Update();
            }
        }
    }
}