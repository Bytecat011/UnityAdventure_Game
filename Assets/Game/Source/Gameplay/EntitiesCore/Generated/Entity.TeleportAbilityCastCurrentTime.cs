namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeleportAbility.TeleportAbilityCastCurrentTime TeleportAbilityCastCurrentTimeC => GetComponent<Game.Gameplay.Features.TeleportAbility.TeleportAbilityCastCurrentTime>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> TeleportAbilityCastCurrentTime => TeleportAbilityCastCurrentTimeC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityCastCurrentTime()
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityCastCurrentTime() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityCastCurrentTime(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityCastCurrentTime() {Value = value});
		}

	}
}
