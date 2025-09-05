namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Sensors.IsTouchAnotherTeam IsTouchAnotherTeamC => GetComponent<Game.Gameplay.Features.Sensors.IsTouchAnotherTeam>();

		public Game.Utility.Reactive.ReactiveVariable<System.Boolean> IsTouchAnotherTeam => IsTouchAnotherTeamC.Value;

		public bool TryGetIsTouchAnotherTeam(out Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Sensors.IsTouchAnotherTeam component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddIsTouchAnotherTeam()
		{
			return AddComponent(new Game.Gameplay.Features.Sensors.IsTouchAnotherTeam() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Boolean>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddIsTouchAnotherTeam(Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new Game.Gameplay.Features.Sensors.IsTouchAnotherTeam() {Value = value});
		}

	}
}
