namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Stats.StatsEffects StatsEffectsC => GetComponent<Game.Gameplay.Features.Stats.StatsEffects>();

		public Game.Gameplay.Features.Stats.StatsEffectsList StatsEffects => StatsEffectsC.Value;

		public bool TryGetStatsEffects(out Game.Gameplay.Features.Stats.StatsEffectsList value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Stats.StatsEffects component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Gameplay.Features.Stats.StatsEffectsList);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddStatsEffects()
		{
			return AddComponent(new Game.Gameplay.Features.Stats.StatsEffects() {Value = new Game.Gameplay.Features.Stats.StatsEffectsList() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddStatsEffects(Game.Gameplay.Features.Stats.StatsEffectsList value)
		{
			return AddComponent(new Game.Gameplay.Features.Stats.StatsEffects() {Value = value});
		}

	}
}
