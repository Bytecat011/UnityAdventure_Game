namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Stats.BaseStats BaseStatsC => GetComponent<Game.Gameplay.Features.Stats.BaseStats>();

		public System.Collections.Generic.Dictionary<Game.Gameplay.Features.Stats.StatTypes, System.Single> BaseStats => BaseStatsC.Value;

		public bool TryGetBaseStats(out System.Collections.Generic.Dictionary<Game.Gameplay.Features.Stats.StatTypes, System.Single> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Stats.BaseStats component);
			if(result)
				value = component.Value;
			else
				value = default(System.Collections.Generic.Dictionary<Game.Gameplay.Features.Stats.StatTypes, System.Single>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddBaseStats()
		{
			return AddComponent(new Game.Gameplay.Features.Stats.BaseStats() {Value = new System.Collections.Generic.Dictionary<Game.Gameplay.Features.Stats.StatTypes, System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddBaseStats(System.Collections.Generic.Dictionary<Game.Gameplay.Features.Stats.StatTypes, System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Stats.BaseStats() {Value = value});
		}

	}
}
