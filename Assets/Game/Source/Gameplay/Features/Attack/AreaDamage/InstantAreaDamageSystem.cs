using System;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Gameplay.Features.ApplyDamage;
using Game.Utility;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Attack.AreaDamage
{
    public class InstantAreaDamageSystem : IInitializableSystem, IDisposableSystem
    {
        private readonly CollidersRegistryService _collidersRegistryService;
        private readonly Buffer<Collider> _colliderBuffer = new Buffer<Collider>(64);
        
        private ReactiveEvent<Vector3> _attackRequest;
        private ReactiveVariable<float> _damage;
        private ReactiveVariable<float> _range;
        private LayerMask _layerMask;
        private CapsuleCollider _body;

        private IDisposable _requestSubscription;

        public InstantAreaDamageSystem(CollidersRegistryService collidersRegistryService)
        {
            _collidersRegistryService = collidersRegistryService;
        }

        public void OnInit(Entity entity)
        {
            _attackRequest = entity.AreaDamageRequest;
            _damage = entity.AreaDamage;
            _range = entity.AreaDamageRange;
            _layerMask = entity.AreaDamageLayerMask;
            _body = entity.BodyCollider;

            _requestSubscription = _attackRequest.Subscribe(OnAttackRequest);
        }

        private void OnAttackRequest(Vector3 position)
        {
            _colliderBuffer.Count = Physics.OverlapSphereNonAlloc(
                position, 
                _range.Value, 
                _colliderBuffer.Items, 
                _layerMask);

            _colliderBuffer.TryRemove(_body);
            
            for (int i = 0; i < _colliderBuffer.Count; i++)
            {
                var contactEntity = _collidersRegistryService.GetBy(_colliderBuffer.Items[i]);
                if (contactEntity != null && contactEntity.HasComponent<TakeDamageRequest>())
                {
                    contactEntity.TakeDamageRequest.Notify(_damage.Value);
                }
            }
            
            Debug.Log($"Area damage on {position.ToString()} hit {_colliderBuffer.Count} targets");
        }

        public void OnDispose()
        {
            _requestSubscription.Dispose();
        }
    }
}