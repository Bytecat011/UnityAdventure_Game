namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeleportAbility.TeleportAbilityEndEvent TeleportAbilityEndEventC => GetComponent<Game.Gameplay.Features.TeleportAbility.TeleportAbilityEndEvent>();

		public Game.Utility.Reactive.ReactiveEvent TeleportAbilityEndEvent => TeleportAbilityEndEventC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityEndEvent()
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityEndEvent() {Value = new Game.Utility.Reactive.ReactiveEvent() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityEndEvent(Game.Utility.Reactive.ReactiveEvent value)
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityEndEvent() {Value = value});
		}

	}
}
