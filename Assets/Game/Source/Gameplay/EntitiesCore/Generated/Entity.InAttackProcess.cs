namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.InAttackProcess InAttackProcessC => GetComponent<Game.Gameplay.Features.Attack.InAttackProcess>();

		public Game.Utility.Reactive.ReactiveVariable<System.Boolean> InAttackProcess => InAttackProcessC.Value;

		public bool TryGetInAttackProcess(out Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Attack.InAttackProcess component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

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
