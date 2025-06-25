using System.Collections;
using UnityEngine;

namespace Game.Utility.CoroutineManagement
{
    public interface ICoroutineRunner
    {
        Coroutine StartTask(IEnumerator coroutineFunction);

        void StopTask(Coroutine coroutine);
    }
}