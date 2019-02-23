using Player;
using UnityEngine;

namespace Units
{
    public abstract class UnitData : ScriptableObject
    {
        public float Population;
        public float Income;
        public float Power;
       

        public abstract IUnit CreateUnit(PlayerData playerData, UnitManager unitManager);
    }

    public abstract class UnitData<TUnit> : UnitData where TUnit : IUnit
    {
        public override IUnit CreateUnit(PlayerData playerData, UnitManager unitManager)
        {
            return OnCreateUnit(playerData, unitManager);
        }
        
        protected abstract TUnit OnCreateUnit(PlayerData playerData, UnitManager unitManager);
    }
}