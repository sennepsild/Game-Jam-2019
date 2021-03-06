using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    [CreateAssetMenu(fileName = "EventTypeData", menuName = "Event type data", order = 53)]
    public class EventTypeData : ScriptableObject
    {
        public EventType EventType;
        public List<EventData> CardsInEvent;
    }
    
    [System.Serializable]
    public class EventData
    {
        public string Name;
        public string Description;
        public List<CardData> EventCards;
    }

    public enum EventType
    {
        Market,
        Immigration,
        Population,
        Military,
        Building,
        Resources,
        Combat,
        NoEvent
    }
}