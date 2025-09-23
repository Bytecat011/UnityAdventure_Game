namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Stats.ModifiedStats ModifiedStatsC => GetComponent<Game.Gameplay.Features.Stats.ModifiedStats>();

		public System.Collections.Generic.Dictionary<Game.Gameplay.Features.Stats.StatTypes, System.Single> ModifiedStats => ModifiedStatsC.Value;

		public bool TryGetModifiedStats(out System.Collections.Generic.Dictionary<Game.Gameplay.Features.Stats.StatTypes, System.Single> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Stats.ModifiedStats component);
			if(result)
				value = component.Value;
			else
				value = default(System.Collections.Generic.Dictionary<Game.Gameplay.Features.Stats.StatTypes, System.Single>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddModifiedStats()
		{
			return AddComponent(new Game.Gameplay.Features.Stats.ModifiedStats() {Value = new System.Collections.Generic.Dictionary<Game.Gameplay.Features.Stats.StatTypes, System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddModifiedStats(System.Collections.Generic.Dictionary<Game.Gameplay.Features.Stats.StatTypes, System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Stats.ModifiedStats() {Value = value});
		}

	}
}
