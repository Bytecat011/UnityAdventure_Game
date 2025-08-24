namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeleportAbility.InTeleportAbilityCastProcess InTeleportAbilityCastProcessC => GetComponent<Game.Gameplay.Features.TeleportAbility.InTeleportAbilityCastProcess>();

		public Game.Utility.Reactive.ReactiveVariable<System.Boolean> InTeleportAbilityCastProcess => InTeleportAbilityCastProcessC.Value;

		public bool TryGetInTeleportAbilityCastProcess(out Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.TeleportAbility.InTeleportAbilityCastProcess component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

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
