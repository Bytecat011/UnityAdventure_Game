namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Energy.MaximumEnergy MaximumEnergyC => GetComponent<Game.Gameplay.Features.Energy.MaximumEnergy>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> MaximumEnergy => MaximumEnergyC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddMaximumEnergy()
		{
			return AddComponent(new Game.Gameplay.Features.Energy.MaximumEnergy() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddMaximumEnergy(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Energy.MaximumEnergy() {Value = value});
		}

	}
}
