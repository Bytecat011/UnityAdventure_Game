using System.Collections;
using UnityEngine;

namespace Game.Utility.CoroutineManagment
{
    public interface ICoroutineRunner
    {
        Coroutine StartTask(IEnumerator coroutineFunction);

        void StopTask(Coroutine coroutine);
    }
}