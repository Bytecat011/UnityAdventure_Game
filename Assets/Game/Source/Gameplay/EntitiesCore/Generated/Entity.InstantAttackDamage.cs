namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.InstantAttackDamage InstantAttackDamageC => GetComponent<Game.Gameplay.Features.Attack.InstantAttackDamage>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> InstantAttackDamage => InstantAttackDamageC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddInstantAttackDamage()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.InstantAttackDamage() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddInstantAttackDamage(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.InstantAttackDamage() {Value = value});
		}

	}
}
