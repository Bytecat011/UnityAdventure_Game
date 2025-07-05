namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Rotation.RotationComponent RotationC => GetComponent<Game.Gameplay.Features.Rotation.RotationComponent>();

		public Game.Utility.Reactive.ReactiveVariable<UnityEngine.Quaternion> Rotation => RotationC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddRotation()
		{
			return AddComponent(new Game.Gameplay.Features.Rotation.RotationComponent() {Value = new Game.Utility.Reactive.ReactiveVariable<UnityEngine.Quaternion>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddRotation(Game.Utility.Reactive.ReactiveVariable<UnityEngine.Quaternion> value)
		{
			return AddComponent(new Game.Gameplay.Features.Rotation.RotationComponent() {Value = value});
		}

	}
}
