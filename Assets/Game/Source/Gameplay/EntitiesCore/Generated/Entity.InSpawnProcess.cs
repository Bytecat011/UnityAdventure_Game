namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.SpawnFeature.InSpawnProcess InSpawnProcessC => GetComponent<Game.Gameplay.Features.SpawnFeature.InSpawnProcess>();

		public Game.Utility.Reactive.ReactiveVariable<System.Boolean> InSpawnProcess => InSpawnProcessC.Value;

		public bool TryGetInSpawnProcess(out Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.SpawnFeature.InSpawnProcess component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddInSpawnProcess()
		{
			return AddComponent(new Game.Gameplay.Features.SpawnFeature.InSpawnProcess() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Boolean>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddInSpawnProcess(Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new Game.Gameplay.Features.SpawnFeature.InSpawnProcess() {Value = value});
		}

	}
}
