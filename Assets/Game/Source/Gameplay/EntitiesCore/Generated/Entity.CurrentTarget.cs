namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.AI.CurrentTarget CurrentTargetC => GetComponent<Game.Gameplay.Features.AI.CurrentTarget>();

		public Game.Utility.Reactive.ReactiveVariable<Game.Gameplay.EntitiesCore.Entity> CurrentTarget => CurrentTargetC.Value;

		public bool TryGetCurrentTarget(out Game.Utility.Reactive.ReactiveVariable<Game.Gameplay.EntitiesCore.Entity> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.AI.CurrentTarget component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<Game.Gameplay.EntitiesCore.Entity>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddCurrentTarget()
		{
			return AddComponent(new Game.Gameplay.Features.AI.CurrentTarget() {Value = new Game.Utility.Reactive.ReactiveVariable<Game.Gameplay.EntitiesCore.Entity>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddCurrentTarget(Game.Utility.Reactive.ReactiveVariable<Game.Gameplay.EntitiesCore.Entity> value)
		{
			return AddComponent(new Game.Gameplay.Features.AI.CurrentTarget() {Value = value});
		}

	}
}
