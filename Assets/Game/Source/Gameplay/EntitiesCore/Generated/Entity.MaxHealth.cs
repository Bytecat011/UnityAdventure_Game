namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.LifeCycle.MaxHealth MaxHealthC => GetComponent<Game.Gameplay.Features.LifeCycle.MaxHealth>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> MaxHealth => MaxHealthC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddMaxHealth()
		{
			return AddComponent(new Game.Gameplay.Features.LifeCycle.MaxHealth() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddMaxHealth(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.LifeCycle.MaxHealth() {Value = value});
		}

	}
}
