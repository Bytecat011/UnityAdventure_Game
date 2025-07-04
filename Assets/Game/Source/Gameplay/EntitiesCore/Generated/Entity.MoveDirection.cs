namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Movement.MoveDirection MoveDirectionC => GetComponent<Game.Gameplay.Features.Movement.MoveDirection>();

		public Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3> MoveDirection => MoveDirectionC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddMoveDirection()
		{
			return AddComponent(new Game.Gameplay.Features.Movement.MoveDirection() {Value = new Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddMoveDirection(Game.Utility.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			return AddComponent(new Game.Gameplay.Features.Movement.MoveDirection() {Value = value});
		}

	}
}
