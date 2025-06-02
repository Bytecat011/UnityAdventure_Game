using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public Coroutine StartTask(IEnumerator coroutineFunction) => StartCoroutine(coroutineFunction);

    public void StopTask(Coroutine coroutine) => StopCoroutine(coroutine);
}
