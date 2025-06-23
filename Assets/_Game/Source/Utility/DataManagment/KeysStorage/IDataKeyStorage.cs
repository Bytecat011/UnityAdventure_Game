namespace Game.Utility.DataManagment.KeysStorage
{
    public interface IDataKeyStarage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }
}