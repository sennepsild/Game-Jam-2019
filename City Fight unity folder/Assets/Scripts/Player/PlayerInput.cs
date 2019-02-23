using System;
using System.Collections.Generic;
using DefaultNamespace;
using Extensions;
using SinputSystems;
using UnityEngine;

namespace Player
{
    public class PlayerInput
    {
        private string[] INPUT_NAMES_TO_CHECK_FOR = { Controls.AButton, Controls.BButton, Controls.XButton, Controls.YButton};
        
        public event Action<string> InputPressed;

        private PlayerData _playerData;

        public PlayerInput(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void Update()
        {
            foreach (var inputNameToCheckFor in INPUT_NAMES_TO_CHECK_FOR)
            {
                if (Sinput.GetButtonDown(inputNameToCheckFor, _playerData.InputDeviceSlot))
                {
                    InvokeInputPressed(inputNameToCheckFor);
                }
            }
        }

        private void InvokeInputPressed(string inputPressedName)
        {
            if (InputPressed != null)
            {
                InputPressed.Invoke(inputPressedName);
            }
        }
    }
}