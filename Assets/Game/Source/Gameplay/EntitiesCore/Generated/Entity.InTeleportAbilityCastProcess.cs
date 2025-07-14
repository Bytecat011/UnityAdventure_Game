namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeleportAbility.InTeleportAbilityCastProcess InTeleportAbilityCastProcessC => GetComponent<Game.Gameplay.Features.TeleportAbility.InTeleportAbilityCastProcess>();

		public Game.Utility.Reactive.ReactiveVariable<System.Boolean> InTeleportAbilityCastProcess => InTeleportAbilityCastProcessC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddInTeleportAbilityCastProcess()
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.InTeleportAbilityCastProcess() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Boolean>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddInTeleportAbilityCastProcess(Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.InTeleportAbilityCastProcess() {Value = value});
		}

	}
}
