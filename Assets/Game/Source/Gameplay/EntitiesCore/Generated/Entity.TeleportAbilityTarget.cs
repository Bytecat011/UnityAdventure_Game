namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeleportAbility.TeleportAbilityTarget TeleportAbilityTargetC => GetComponent<Game.Gameplay.Features.TeleportAbility.TeleportAbilityTarget>();

		public Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3> TeleportAbilityTarget => TeleportAbilityTargetC.Value;

		public bool TryGetTeleportAbilityTarget(out Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.TeleportAbility.TeleportAbilityTarget component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityTarget()
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityTarget() {Value = new Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddTeleportAbilityTarget(Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			return AddComponent(new Game.Gameplay.Features.TeleportAbility.TeleportAbilityTarget() {Value = value});
		}

	}
}
