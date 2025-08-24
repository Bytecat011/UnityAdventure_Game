namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeleportAbility.TeleportAbilityEndEvent TeleportAbilityEndEventC => GetComponent<Game.Gameplay.Features.TeleportAbility.TeleportAbilityEndEvent>();

		public Game.Utility.Reactive.ReactiveEvent TeleportAbilityEndEvent => TeleportAbilityEndEventC.Value;

		public bool TryGetTeleportAbilityEndEvent(out Game.Utility.Reactive.ReactiveEvent value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.TeleportAbility.TeleportAbilityEndEvent component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveEvent);
			return result;
		}

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
