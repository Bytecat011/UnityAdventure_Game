namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Energy.EnergyRestorationInterval EnergyRestorationIntervalC => GetComponent<Game.Gameplay.Features.Energy.EnergyRestorationInterval>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> EnergyRestorationInterval => EnergyRestorationIntervalC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddEnergyRestorationInterval()
		{
			return AddComponent(new Game.Gameplay.Features.Energy.EnergyRestorationInterval() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddEnergyRestorationInterval(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Energy.EnergyRestorationInterval() {Value = value});
		}

	}
}
