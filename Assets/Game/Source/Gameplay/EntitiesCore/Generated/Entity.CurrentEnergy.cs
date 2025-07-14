namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Energy.CurrentEnergy CurrentEnergyC => GetComponent<Game.Gameplay.Features.Energy.CurrentEnergy>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> CurrentEnergy => CurrentEnergyC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddCurrentEnergy()
		{
			return AddComponent(new Game.Gameplay.Features.Energy.CurrentEnergy() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddCurrentEnergy(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Energy.CurrentEnergy() {Value = value});
		}

	}
}
