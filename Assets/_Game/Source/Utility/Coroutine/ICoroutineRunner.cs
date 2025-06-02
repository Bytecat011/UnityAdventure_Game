using System.Collections;
using UnityEngine;

public interface ICoroutineRunner
{
    Coroutine StartTask(IEnumerator coroutineFunction);

    void StopTask(Coroutine coroutine);
}