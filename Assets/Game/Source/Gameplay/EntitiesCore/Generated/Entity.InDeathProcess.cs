namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.LifeCycle.InDeathProcess InDeathProcessC => GetComponent<Game.Gameplay.Features.LifeCycle.InDeathProcess>();

		public Game.Utility.Reactive.ReactiveVariable<System.Boolean> InDeathProcess => InDeathProcessC.Value;

		public bool TryGetInDeathProcess(out Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.LifeCycle.InDeathProcess component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddInDeathProcess()
		{
			return AddComponent(new Game.Gameplay.Features.LifeCycle.InDeathProcess() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Boolean>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddInDeathProcess(Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new Game.Gameplay.Features.LifeCycle.InDeathProcess() {Value = value});
		}

	}
}
