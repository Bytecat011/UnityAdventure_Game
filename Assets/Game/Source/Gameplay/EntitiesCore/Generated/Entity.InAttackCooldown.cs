namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.InAttackCooldown InAttackCooldownC => GetComponent<Game.Gameplay.Features.Attack.InAttackCooldown>();

		public Game.Utility.Reactive.ReactiveVariable<System.Boolean> InAttackCooldown => InAttackCooldownC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddInAttackCooldown()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.InAttackCooldown() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Boolean>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddInAttackCooldown(Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.InAttackCooldown() {Value = value});
		}

	}
}
