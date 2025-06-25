namespace Game.Utility.DataManagement.KeysStorage
{
    public interface IDataKeyStarage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }
}