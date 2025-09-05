namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.MainHero.IsMainHero IsMainHeroC => GetComponent<Game.Gameplay.Features.MainHero.IsMainHero>();

		public Game.Gameplay.EntitiesCore.Entity AddIsMainHero()
		{
			return AddComponent(new Game.Gameplay.Features.MainHero.IsMainHero() );
		}

	}
}
