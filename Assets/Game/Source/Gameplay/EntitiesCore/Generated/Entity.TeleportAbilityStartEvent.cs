namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeleportAbility.TeleportAbilityStartEvent TeleportAbilityStartEventC => GetComponent<Game.Gameplay.Features.TeleportAbility.TeleportAbilityStartEvent>();

		public Game.Utility.Reactive.ReactiveEvent TeleportAbilityStartEvent => TeleportAbilityStartEventC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityStartEvent()
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityStartEvent() {Value = new Game.Utility.Reactive.ReactiveEvent() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityStartEvent(Game.Utility.Reactive.ReactiveEvent value)
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityStartEvent() {Value = value});
		}

	}
}
