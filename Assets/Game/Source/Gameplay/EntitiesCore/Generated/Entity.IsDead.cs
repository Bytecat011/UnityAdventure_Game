namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.LifeCycle.IsDead IsDeadC => GetComponent<Game.Gameplay.Features.LifeCycle.IsDead>();

		public Game.Utility.Reactive.ReactiveVariable<System.Boolean> IsDead => IsDeadC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddIsDead()
		{
			return AddComponent(new Game.Gameplay.Features.LifeCycle.IsDead() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Boolean>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddIsDead(Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new Game.Gameplay.Features.LifeCycle.IsDead() {Value = value});
		}

	}
}
