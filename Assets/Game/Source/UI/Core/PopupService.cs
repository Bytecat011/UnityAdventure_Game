using System;
using System.Collections.Generic;
using Game.UI.LevelsMenuPopup;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace Game.UI.Core
{
    public abstract class PopupService : IDisposable
    {
        protected readonly ViewsFactory ViewsFactory;
        
        private readonly ProjectPresentersFactory _presentersFactory;

        private readonly Dictionary<PopupPresenterBase, PopupInfo> _presenterToInfo = new();
        
        protected PopupService(
            ViewsFactory viewsFactory,
            ProjectPresentersFactory presentersFactory)
        {
            ViewsFactory = viewsFactory;
            _presentersFactory = presentersFactory;
        }
        
        protected abstract Transform PopupLayer { get; }

        public LevelsMenuPopupPresenter OpenLevelsMenuPopup()
        {
            LevelsMenuPopupView view = ViewsFactory.Create<LevelsMenuPopupView>(ViewIDs.LevelsMenuPopup, PopupLayer);

            LevelsMenuPopupPresenter popup = _presentersFactory.CreateLevelMenuPopupPresenter(view);
            
            OnPopupCreated(popup, view);

            return popup;
        }
        
        public TestPopupPresenter OpenTestPopup(Action closeCallback = null)
        {
            TestPopupView view = ViewsFactory.Create<TestPopupView>(ViewIDs.TestPopup, PopupLayer);

            TestPopupPresenter popup = _presentersFactory.CreateTestPopupPresenter(view);
            
            OnPopupCreated(popup, view, closeCallback);

            return popup;
        }
        
        public void ClosePopup(PopupPresenterBase popup)
        {
            popup.CloseRequest -= ClosePopup;
            
            popup.Hide(() =>
            {
                _presenterToInfo[popup].CloseCallback?.Invoke();
                
                DisposeFor(popup);
                _presenterToInfo.Remove(popup);
            });
        }

        public void Dispose()
        {
            foreach (PopupPresenterBase popup in _presenterToInfo.Keys)
            {
                popup.CloseRequest -= ClosePopup;
                DisposeFor(popup);
            }
            
            _presenterToInfo.Clear();
        }
        
        protected void OnPopupCreated(
            PopupPresenterBase popup,
            PopupViewBase view,
            Action closeCallback = null)
        {
            PopupInfo popupInfo = new PopupInfo(view, closeCallback);
            
            _presenterToInfo.Add(popup, popupInfo);
            popup.Initialize();
            popup.Show();

            popup.CloseRequest += ClosePopup;
        }
        
        private void DisposeFor(PopupPresenterBase popup)
        {
            popup.Dispose();
            ViewsFactory.Release(_presenterToInfo[popup].View);
        }

        private class PopupInfo
        {
            public PopupInfo(PopupViewBase view, Action closeCallback)
            {
                View = view;
                CloseCallback = closeCallback;
            }

            public PopupViewBase View { get; }
            public Action CloseCallback { get; }
        }
    }
}