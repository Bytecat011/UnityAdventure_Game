namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.TowerAttack.TowerAttackDamage TowerAttackDamageC => GetComponent<Game.Gameplay.Features.Attack.TowerAttack.TowerAttackDamage>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> TowerAttackDamage => TowerAttackDamageC.Value;

		public bool TryGetTowerAttackDamage(out Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Attack.TowerAttack.TowerAttackDamage component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddTowerAttackDamage()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.TowerAttack.TowerAttackDamage() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddTowerAttackDamage(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.TowerAttack.TowerAttackDamage() {Value = value});
		}

	}
}
