using System.Collections.Generic;
using Units;
using UnityEngine;

namespace DefaultNamespace
{
    public class DebugManager : MonoBehaviour
    {
        [SerializeField]
        private List<UnitData> _unitDataEntriesToAddAtStart;

        private void Start()
        {
            foreach (var playerDataEntry in Game.Instance.PlayerDataEntries)
            {
                playerDataEntry.UnitManager.AddUnits(_unitDataEntriesToAddAtStart);
            }
        }
    }
}