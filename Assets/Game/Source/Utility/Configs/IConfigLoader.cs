using System;
using System.Collections;
using System.Collections.Generic;

namespace Game.Utility.Configs
{
    public interface IConfigLoader
    {
        IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigLoaded);
    }
}