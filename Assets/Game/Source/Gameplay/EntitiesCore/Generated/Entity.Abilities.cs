namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Abilities.AbilitiesComponent AbilitiesC => GetComponent<Game.Gameplay.Features.Abilities.AbilitiesComponent>();

		public Game.Gameplay.Features.Abilities.AbilitiesList Abilities => AbilitiesC.Value;

		public bool TryGetAbilities(out Game.Gameplay.Features.Abilities.AbilitiesList value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Abilities.AbilitiesComponent component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Gameplay.Features.Abilities.AbilitiesList);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddAbilities()
		{
			return AddComponent(new Game.Gameplay.Features.Abilities.AbilitiesComponent() {Value = new Game.Gameplay.Features.Abilities.AbilitiesList() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddAbilities(Game.Gameplay.Features.Abilities.AbilitiesList value)
		{
			return AddComponent(new Game.Gameplay.Features.Abilities.AbilitiesComponent() {Value = value});
		}

	}
}
