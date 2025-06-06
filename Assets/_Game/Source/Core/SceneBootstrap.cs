using Game.Core.DI;
using Game.Utility.SceneManagment;
using System.Collections;
using UnityEngine;

namespace Game.Core
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs);

        public abstract IEnumerator Initialize();

        public abstract void Run();
    }
}