namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.LevelUpFeature.Level LevelC => GetComponent<Game.Gameplay.Features.LevelUpFeature.Level>();

		public Game.Utility.Reactive.ReactiveVariable<System.Int32> Level => LevelC.Value;

		public bool TryGetLevel(out Game.Utility.Reactive.ReactiveVariable<System.Int32> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.LevelUpFeature.Level component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Int32>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddLevel()
		{
			return AddComponent(new Game.Gameplay.Features.LevelUpFeature.Level() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Int32>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddLevel(Game.Utility.Reactive.ReactiveVariable<System.Int32> value)
		{
			return AddComponent(new Game.Gameplay.Features.LevelUpFeature.Level() {Value = value});
		}

	}
}
