using System.Collections.Generic;
using Game.Meta.Features.Resources;
using Game.UI.CommonViews;
using Game.UI.Core;

namespace Game.UI.Resources
{
    public class ResourcesPresenter : IPresenter
    {
        private readonly ResourceStorage _resourceStorage;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private readonly IconTextListView _view;

        private readonly List<ResourcePresenter> _resourcesPresenters = new();
        
        public ResourcesPresenter(
            ResourceStorage resourceStorage,
            ProjectPresentersFactory presentersFactory, 
            ViewsFactory viewsFactory, 
            IconTextListView view)
        {
            _resourceStorage = resourceStorage;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        public void Initialize()
        {
            foreach (var resourceType in _resourceStorage.AvailableResources())
            {
                IconTextView resourceView = _viewsFactory.Create<IconTextView>(ViewIDs.ResourceView);
                
                _view.Add(resourceView);

                ResourcePresenter resourcePresenter = _presentersFactory.CreateResourcePresenter(
                    resourceView,
                    _resourceStorage.GetResource(resourceType),
                    resourceType);
                
                resourcePresenter.Initialize();
                _resourcesPresenters.Add(resourcePresenter);
            }
        }

        public void Dispose()
        {
            foreach (var resourcePresenter in _resourcesPresenters)
            {
                _view.Remove(resourcePresenter.View);
                _viewsFactory.Release(resourcePresenter.View);
                resourcePresenter.Dispose();
            }
            
            _resourcesPresenters.Clear();
        }
    }
}