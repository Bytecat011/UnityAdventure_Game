namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.InAttackProcess InAttackProcessC => GetComponent<Game.Gameplay.Features.Attack.InAttackProcess>();

		public Game.Utility.Reactive.ReactiveVariable<System.Boolean> InAttackProcess => InAttackProcessC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddInAttackProcess()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.InAttackProcess() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Boolean>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddInAttackProcess(Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.InAttackProcess() {Value = value});
		}

	}
}
