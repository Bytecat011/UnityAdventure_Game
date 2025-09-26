namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.TowerAttack.TowerAttackTargetPoint TowerAttackTargetPointC => GetComponent<Game.Gameplay.Features.Attack.TowerAttack.TowerAttackTargetPoint>();

		public Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3> TowerAttackTargetPoint => TowerAttackTargetPointC.Value;

		public bool TryGetTowerAttackTargetPoint(out Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Attack.TowerAttack.TowerAttackTargetPoint component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddTowerAttackTargetPoint()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.TowerAttack.TowerAttackTargetPoint() {Value = new Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddTowerAttackTargetPoint(Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.TowerAttack.TowerAttackTargetPoint() {Value = value});
		}

	}
}
