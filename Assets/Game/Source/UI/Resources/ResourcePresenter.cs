using Game.Configs;
using Game.Meta.Features.Resources;
using Game.UI.CommonViews;
using Game.UI.Core;
using Game.Utility.Reactive;

namespace Game.UI.Resources
{
    public class ResourcePresenter : IPresenter
    {
        private readonly IReactiveVariable<int> _resource;
        private readonly ResourceType _resourceType;
        private readonly ResourceIconsConfig _resourceIconsConfig;
        private readonly IconTextView _view;
        
        private ISubscription _resourceChangedSubscription;
        
        public ResourcePresenter(
            IReactiveVariable<int> resource, 
            ResourceType resourceType, 
            ResourceIconsConfig resourceIconsConfig,
            IconTextView view)
        {
            _resource = resource;
            _resourceType = resourceType;
            _resourceIconsConfig = resourceIconsConfig;
            _view = view;
        }

        public IconTextView View => _view;

        public void Initialize()
        {
            UpdateValue(_resource.Value);
            _view.SetIcon(_resourceIconsConfig.GetSpriteFor(_resourceType));

            _resourceChangedSubscription = _resource.Subscribe(OnResourceChanged);
        }

        public void Dispose()
        {
            _resourceChangedSubscription.Unsubscribe();    
        }
        
        private void OnResourceChanged(int oldValue, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value)
        {
            _view.SetText(value.ToString());
        }
    }
}