namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.LevelUpFeature.Experience ExperienceC => GetComponent<Game.Gameplay.Features.LevelUpFeature.Experience>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> Experience => ExperienceC.Value;

		public bool TryGetExperience(out Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.LevelUpFeature.Experience component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddExperience()
		{
			return AddComponent(new Game.Gameplay.Features.LevelUpFeature.Experience() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddExperience(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.LevelUpFeature.Experience() {Value = value});
		}

	}
}
