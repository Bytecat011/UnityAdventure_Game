using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Mono;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.SpawnFeature
{
    [RequireComponent(typeof(Animator))]
    public class SpawnProcessView : EntityView
    {
        private readonly int SpawningProcessKey = Animator.StringToHash("IsSpawnProcess");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _spawnEffectPrefab;
        
        private IReactiveVariable<bool> _inSpawnProcess;
        private Transform _entityTransform;
        
        private IDisposable _inSpawnProcessChangedSubscription;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _inSpawnProcess = entity.InSpawnProcess;
            _entityTransform = entity.Transform;

            _inSpawnProcessChangedSubscription = _inSpawnProcess.Subscribe(OnSpawnProcessChanged);
            UpdateSpawnProcessKey(_inSpawnProcess.Value);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            
            _inSpawnProcessChangedSubscription?.Dispose();
        }

        private void OnSpawnProcessChanged(bool _, bool inSpawnProcessValue) => UpdateSpawnProcessKey(inSpawnProcessValue);

        private void UpdateSpawnProcessKey(bool inSpawnProcessValue)
        {
            _animator.SetBool(SpawningProcessKey, inSpawnProcessValue);
            
            if (inSpawnProcessValue)
                Instantiate(_spawnEffectPrefab, _entityTransform.position, _spawnEffectPrefab.transform.rotation, null);
        }
    }
}