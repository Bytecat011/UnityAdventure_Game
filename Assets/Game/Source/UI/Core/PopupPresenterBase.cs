using System;
using System.Collections;
using DG.Tweening;
using Game.Utility.CoroutineManagement;
using UnityEngine;

namespace Game.UI.Core
{
    public abstract class PopupPresenterBase : IPresenter
    {
        public event Action<PopupPresenterBase> CloseRequest;
        
        private readonly ICoroutineRunner _coroutineRunner;

        private Coroutine _activeTask;
        
        protected PopupPresenterBase(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        protected abstract PopupViewBase PopupView { get; }
        
        public virtual void Initialize()
        {
        }

        public virtual void Dispose()
        {
            KillProcess();
            
            PopupView.CloseRequest -= OnCloseRequest;
        }

        public void Show()
        {
            KillProcess();
            
            _activeTask = _coroutineRunner.StartTask(ShowTask());
        }

        public void Hide(Action callback = null)
        {
            KillProcess();
            
            _activeTask = _coroutineRunner.StartTask(HideTask(callback));
        }
        
        protected virtual void OnPreShow()
        {
            PopupView.CloseRequest += OnCloseRequest;
        }

        protected virtual void OnPostShow() { }

        protected virtual void OnPreHide()
        {
            PopupView.CloseRequest -= OnCloseRequest;
        }

        protected virtual void OnPostHide() { }

        protected void OnCloseRequest() => CloseRequest?.Invoke(this);

        private IEnumerator ShowTask()
        {
            OnPreShow();
            yield return PopupView.Show().WaitForCompletion();
            OnPostShow();
        }
        
        private IEnumerator HideTask(Action callback = null)
        {
            OnPreHide();
            yield return PopupView.Hide().WaitForCompletion();
            OnPostHide();
            
            callback?.Invoke();
        }
        
        private void KillProcess()
        {
            if (_activeTask != null)
                _coroutineRunner.StopTask(_activeTask);
        }
    }
}