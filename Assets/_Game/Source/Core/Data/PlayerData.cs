using Game.Meta.Features.Resources;
using Game.Utility.DataManagment;
using System.Collections.Generic;

namespace Game.Data
{
    public class PlayerData : ISaveData
    {
        public Dictionary<ResourceType, int> ResourceData;
    }
}