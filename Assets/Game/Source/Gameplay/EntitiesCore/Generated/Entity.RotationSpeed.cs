namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Movement.RotationSpeed RotationSpeedC => GetComponent<Game.Gameplay.Features.Movement.RotationSpeed>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> RotationSpeed => RotationSpeedC.Value;

		public bool TryGetRotationSpeed(out Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Movement.RotationSpeed component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddRotationSpeed()
		{
			return AddComponent(new Game.Gameplay.Features.Movement.RotationSpeed() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddRotationSpeed(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Movement.RotationSpeed() {Value = value});
		}

	}
}
