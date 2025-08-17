namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Movement.RotationDirection RotationDirectionC => GetComponent<Game.Gameplay.Features.Movement.RotationDirection>();

		public Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3> RotationDirection => RotationDirectionC.Value;

		public bool TryGetRotationDirection(out Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Movement.RotationDirection component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddRotationDirection()
		{
			return AddComponent(new Game.Gameplay.Features.Movement.RotationDirection() {Value = new Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddRotationDirection(Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			return AddComponent(new Game.Gameplay.Features.Movement.RotationDirection() {Value = value});
		}

	}
}
