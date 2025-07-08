namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.LifeCycle.CurrentHealth CurrentHealthC => GetComponent<Game.Gameplay.Features.LifeCycle.CurrentHealth>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> CurrentHealth => CurrentHealthC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddCurrentHealth()
		{
			return AddComponent(new Game.Gameplay.Features.LifeCycle.CurrentHealth() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddCurrentHealth(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.LifeCycle.CurrentHealth() {Value = value});
		}

	}
}
