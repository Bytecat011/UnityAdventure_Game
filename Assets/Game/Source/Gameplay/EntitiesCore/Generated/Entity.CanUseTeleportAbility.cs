namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeleportAbility.CanUseTeleportAbility CanUseTeleportAbilityC => GetComponent<Game.Gameplay.Features.TeleportAbility.CanUseTeleportAbility>();

		public Game.Utility.Conditions.ICompositeCondition CanUseTeleportAbility => CanUseTeleportAbilityC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddCanUseTeleportAbility(Game.Utility.Conditions.ICompositeCondition value)
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.CanUseTeleportAbility() {Value = value});
		}

	}
}
