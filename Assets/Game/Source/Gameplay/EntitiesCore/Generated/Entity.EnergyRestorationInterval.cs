namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Energy.EnergyRestorationInterval EnergyRestorationIntervalC => GetComponent<Game.Gameplay.Features.Energy.EnergyRestorationInterval>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> EnergyRestorationInterval => EnergyRestorationIntervalC.Value;

		public bool TryGetEnergyRestorationInterval(out Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Energy.EnergyRestorationInterval component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

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
