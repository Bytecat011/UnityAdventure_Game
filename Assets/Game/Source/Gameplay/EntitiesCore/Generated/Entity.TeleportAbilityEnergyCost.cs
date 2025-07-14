namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeleportAbility.TeleportAbilityEnergyCost TeleportAbilityEnergyCostC => GetComponent<Game.Gameplay.Features.TeleportAbility.TeleportAbilityEnergyCost>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> TeleportAbilityEnergyCost => TeleportAbilityEnergyCostC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityEnergyCost()
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityEnergyCost() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityEnergyCost(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityEnergyCost() {Value = value});
		}

	}
}
