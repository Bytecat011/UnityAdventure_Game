using System.Collections;
using UnityEngine;

namespace Game.Utility.CoroutineManagement
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public Coroutine StartTask(IEnumerator coroutineFunction)
        {
            return StartCoroutine(coroutineFunction);
        }

        public void StopTask(Coroutine coroutine) => StopCoroutine(coroutine);
    }
}