using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Game.UI.Core
{
    public abstract class PopupViewBase : MonoBehaviour, IShowableView
    {
        public event Action CloseRequest;
        
        [SerializeField] private CanvasGroup _mainGroup;
        [SerializeField] private Image _anticlicker;
        [SerializeField] private CanvasGroup _body;

        [SerializeField] private PopupAnimationTypes _animationType;

        private float _anticlickerDefaultAlpha;
        
        private Tween _currentAnimation;
        
        private void Awake()
        {
            _mainGroup.alpha = 0;
            _anticlickerDefaultAlpha = _anticlicker.color.a;
        }
        
        public void OnCloseButtonClick() => CloseRequest?.Invoke();

        public Tween Show()
        {
            KillCurrentAnimation();
            
            OnPreShow();

            _mainGroup.alpha = 1;

            Sequence animation = PopupAnimationsCreator
                .CreateShowAnimation(_body, _anticlicker, _animationType, _anticlickerDefaultAlpha);
            
            ModifyShowAnimation(animation);

            animation.OnComplete(OnPostShow);
            
            _currentAnimation = animation.SetUpdate(true).Play();

            return animation;
        }

        public Tween Hide()
        {
            KillCurrentAnimation();
            
            OnPreHide();
            
            Sequence animation = PopupAnimationsCreator
                .CreateHideAnimation(_body, _anticlicker, _animationType, _anticlickerDefaultAlpha);
            
            ModifyHideAnimation(animation);
            
            animation.OnComplete(OnPostHide);
            
            _currentAnimation = animation.SetUpdate(true).Play();
            
            return animation;
        }

        protected virtual void ModifyShowAnimation(Sequence animation) { }
        
        protected virtual void ModifyHideAnimation(Sequence animation) { }
        
        protected virtual void OnPreShow() { }

        protected virtual void OnPostShow() { }
        
        protected virtual void OnPreHide() { }

        protected virtual void OnPostHide() { }

        private void OnDestroy() => KillCurrentAnimation();

        private void KillCurrentAnimation()
        {
            _currentAnimation?.Kill();
        }
    }
}