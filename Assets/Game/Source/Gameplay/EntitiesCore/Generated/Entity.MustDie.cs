namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.LifeCycle.MustDie MustDieC => GetComponent<Game.Gameplay.Features.LifeCycle.MustDie>();

		public Game.Utility.Conditions.ICompositeCondition MustDie => MustDieC.Value;

		public bool TryGetMustDie(out Game.Utility.Conditions.ICompositeCondition value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.LifeCycle.MustDie component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Conditions.ICompositeCondition);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddMustDie(Game.Utility.Conditions.ICompositeCondition value)
		{
			return AddComponent(new Game.Gameplay.Features.LifeCycle.MustDie() {Value = value});
		}

	}
}
