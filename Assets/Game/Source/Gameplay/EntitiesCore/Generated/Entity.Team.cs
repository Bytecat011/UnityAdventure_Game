namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.TeamsFeatures.Team TeamC => GetComponent<Game.Gameplay.Features.TeamsFeatures.Team>();

		public Game.Utility.Reactive.ReactiveVariable<Game.Gameplay.Features.TeamsFeatures.Teams> Team => TeamC.Value;

		public bool TryGetTeam(out Game.Utility.Reactive.ReactiveVariable<Game.Gameplay.Features.TeamsFeatures.Teams> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.TeamsFeatures.Team component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<Game.Gameplay.Features.TeamsFeatures.Teams>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddTeam()
		{
			return AddComponent(new Game.Gameplay.Features.TeamsFeatures.Team() {Value = new Game.Utility.Reactive.ReactiveVariable<Game.Gameplay.Features.TeamsFeatures.Teams>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddTeam(Game.Utility.Reactive.ReactiveVariable<Game.Gameplay.Features.TeamsFeatures.Teams> value)
		{
			return AddComponent(new Game.Gameplay.Features.TeamsFeatures.Team() {Value = value});
		}

	}
}
