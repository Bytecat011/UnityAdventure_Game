namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeleportAbility.TeleportAbilityCastInitialTime TeleportAbilityCastInitialTimeC => GetComponent<Game.Gameplay.Features.TeleportAbility.TeleportAbilityCastInitialTime>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> TeleportAbilityCastInitialTime => TeleportAbilityCastInitialTimeC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityCastInitialTime()
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityCastInitialTime() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityCastInitialTime(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityCastInitialTime() {Value = value});
		}

	}
}
