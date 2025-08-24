namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeleportAbility.TeleportAbilityStartRequest TeleportAbilityStartRequestC => GetComponent<Game.Gameplay.Features.TeleportAbility.TeleportAbilityStartRequest>();

		public Game.Utility.Reactive.ReactiveEvent TeleportAbilityStartRequest => TeleportAbilityStartRequestC.Value;

		public bool TryGetTeleportAbilityStartRequest(out Game.Utility.Reactive.ReactiveEvent value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.TeleportAbility.TeleportAbilityStartRequest component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveEvent);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityStartRequest()
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityStartRequest() {Value = new Game.Utility.Reactive.ReactiveEvent() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityStartRequest(Game.Utility.Reactive.ReactiveEvent value)
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityStartRequest() {Value = value});
		}

	}
}
