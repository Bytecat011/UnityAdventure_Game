namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeleportAbility.TeleportAbilityRange TeleportAbilityRangeC => GetComponent<Game.Gameplay.Features.TeleportAbility.TeleportAbilityRange>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> TeleportAbilityRange => TeleportAbilityRangeC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityRange()
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityRange() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityRange(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityRange() {Value = value});
		}

	}
}
