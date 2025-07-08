namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.LifeCycle.MustSelfRelease MustSelfReleaseC => GetComponent<Game.Gameplay.Features.LifeCycle.MustSelfRelease>();

		public Game.Utility.Conditions.ICompositeCondition MustSelfRelease => MustSelfReleaseC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddMustSelfRelease(Game.Utility.Conditions.ICompositeCondition value)
		{
			return AddComponent(new Game.Gameplay.Features.LifeCycle.MustSelfRelease() {Value = value});
		}

	}
}
