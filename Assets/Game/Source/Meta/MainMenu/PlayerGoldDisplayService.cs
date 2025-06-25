using System.Collections;
using Game.Meta.Features.Resources;
using Game.Utility.CoroutineManagement;
using UnityEngine;

namespace Game.Meta.MainMenu
{
    public class PlayerGoldDisplayService
    {
        private const KeyCode ShowBalanceKeyCode = KeyCode.I;
        
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ResourceStorage _resourceStorage;

        private Coroutine _handleInputTsk;

        public PlayerGoldDisplayService(
            ICoroutineRunner coroutineRunner,
            ResourceStorage resourceStorage)
        {
            _coroutineRunner = coroutineRunner;
            _resourceStorage = resourceStorage;
        }

        public void Start()
        {
            _handleInputTsk = _coroutineRunner.StartTask(HandleInputTask());
        }

        private IEnumerator HandleInputTask()
        {
            Debug.Log($"You can see your gold balance by press {ShowBalanceKeyCode} key");
            
            while (true)
            {
                if (Input.GetKeyDown(ShowBalanceKeyCode))
                    Debug.Log($"You have {_resourceStorage.GetResource(ResourceType.Gold).Value} gold");
                yield return null;
            }
        }

        public void Dispose()
        {
            if (_handleInputTsk != null)
                _coroutineRunner.StopTask(_handleInputTsk);
        }
    }
}