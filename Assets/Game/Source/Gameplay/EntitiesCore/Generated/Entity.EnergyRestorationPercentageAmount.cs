namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Energy.EnergyRestorationPercentageAmount EnergyRestorationPercentageAmountC => GetComponent<Game.Gameplay.Features.Energy.EnergyRestorationPercentageAmount>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> EnergyRestorationPercentageAmount => EnergyRestorationPercentageAmountC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddEnergyRestorationPercentageAmount()
		{
			return AddComponent(new Game.Gameplay.Features.Energy.EnergyRestorationPercentageAmount() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddEnergyRestorationPercentageAmount(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Energy.EnergyRestorationPercentageAmount() {Value = value});
		}

	}
}
