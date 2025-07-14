namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Energy.EnergyRestorationTimer EnergyRestorationTimerC => GetComponent<Game.Gameplay.Features.Energy.EnergyRestorationTimer>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> EnergyRestorationTimer => EnergyRestorationTimerC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddEnergyRestorationTimer()
		{
			return AddComponent(new Game.Gameplay.Features.Energy.EnergyRestorationTimer() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddEnergyRestorationTimer(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Energy.EnergyRestorationTimer() {Value = value});
		}

	}
}
