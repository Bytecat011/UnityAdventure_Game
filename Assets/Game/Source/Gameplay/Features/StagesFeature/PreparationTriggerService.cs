using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.MainHero;
using Game.Utility;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.StagesFeature
{
    public class PreparationTriggerService
    {
        private ReactiveVariable<bool> _hasMainHeroContact = new();
        
        private EntitiesFactory _entitiesFactory;
        private EntitiesWorld _entitiesWorld;

        private Entity _nextStageTrigger;
        private Buffer<Entity> _nextStageTriggerContacts;

        public PreparationTriggerService(
            EntitiesFactory entitiesFactory,
            EntitiesWorld entitiesWorld)
        {
            _entitiesFactory = entitiesFactory;
            _entitiesWorld = entitiesWorld;
        }
        
        public IReactiveVariable<bool> HasMainHeroContact => _hasMainHeroContact;

        public void Create(Vector3 position)
        {
            if (_nextStageTrigger != null)
                throw new InvalidOperationException("Trigger already created");
            
            _nextStageTrigger = _entitiesFactory.CreateContactTrigger(position);
            _nextStageTriggerContacts = _nextStageTrigger.ContactEntitiesBuffer;
        }

        public void Update(float deltaTime)
        {
            if (_nextStageTrigger == null)
                return;
            
            for (int i = 0; i < _nextStageTriggerContacts.Count; i++)
            {
                Entity contact = _nextStageTriggerContacts.Items[i];

                if (contact.HasComponent<IsMainHero>())
                {
                    _hasMainHeroContact.Value = true;
                    return;
                }
            }
            
            _hasMainHeroContact.Value = false;
        }
        
        public void Cleanup()
        {
            _entitiesWorld.Release(_nextStageTrigger);
            _hasMainHeroContact.Value = false;
            _nextStageTrigger = null;
            _nextStageTriggerContacts = null;
        }
    }
}