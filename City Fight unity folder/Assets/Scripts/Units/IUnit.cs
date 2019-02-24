namespace Units
{
    public interface IUnit
    {
        T GetUnitData<T>() where T : UnitData;
        void Turn();
    }
}