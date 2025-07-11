namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Movement.IsMoving IsMovingC => GetComponent<Game.Gameplay.Features.Movement.IsMoving>();

		public Game.Utility.Reactive.ReactiveVariable<System.Boolean> IsMoving => IsMovingC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddIsMoving()
		{
			return AddComponent(new Game.Gameplay.Features.Movement.IsMoving() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Boolean>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddIsMoving(Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new Game.Gameplay.Features.Movement.IsMoving() {Value = value});
		}

	}
}
