namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Movement.MoveSpeed MoveSpeedC => GetComponent<Game.Gameplay.Features.Movement.MoveSpeed>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> MoveSpeed => MoveSpeedC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddMoveSpeed()
		{
			return AddComponent(new Game.Gameplay.Features.Movement.MoveSpeed() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddMoveSpeed(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Movement.MoveSpeed() {Value = value});
		}

	}
}
