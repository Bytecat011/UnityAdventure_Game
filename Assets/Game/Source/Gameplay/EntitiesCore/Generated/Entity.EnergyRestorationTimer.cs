namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Energy.EnergyRestorationTimer EnergyRestorationTimerC => GetComponent<Game.Gameplay.Features.Energy.EnergyRestorationTimer>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> EnergyRestorationTimer => EnergyRestorationTimerC.Value;

		public bool TryGetEnergyRestorationTimer(out Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Energy.EnergyRestorationTimer component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

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
