namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeleportAbility.CanUseTeleportAbility CanUseTeleportAbilityC => GetComponent<Game.Gameplay.Features.TeleportAbility.CanUseTeleportAbility>();

		public Game.Utility.Conditions.ICompositeCondition CanUseTeleportAbility => CanUseTeleportAbilityC.Value;

		public bool TryGetCanUseTeleportAbility(out Game.Utility.Conditions.ICompositeCondition value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.TeleportAbility.CanUseTeleportAbility component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Conditions.ICompositeCondition);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddCanUseTeleportAbility(Game.Utility.Conditions.ICompositeCondition value)
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.CanUseTeleportAbility() {Value = value});
		}

	}
}
