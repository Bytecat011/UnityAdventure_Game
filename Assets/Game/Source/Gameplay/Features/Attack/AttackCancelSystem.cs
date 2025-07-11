using Game.Gameplay.EntitiesCore;
using Game.Gameplay.EntitiesCore.Systems;
using Game.Utility.Conditions;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.Gameplay.Features.Attack
{
    public class AttackCancelSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<bool> _inAttackProcess;
        private ReactiveEvent _attackCancelEvent;
        private ICompositeCondition _mustCancelAttack;


        public void OnInit(Entity entity)
        {
            _inAttackProcess = entity.InAttackProcess;
            _attackCancelEvent = entity.AttackCancelEvent;
            _mustCancelAttack = entity.MustCancelAttack;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inAttackProcess.Value == false)
                return;
            
            if (_mustCancelAttack.Evaluate())
            {
                Debug.Log("Attack canceled");
                _inAttackProcess.Value = false;
                _attackCancelEvent.Notify();
            }
        }
    }
}