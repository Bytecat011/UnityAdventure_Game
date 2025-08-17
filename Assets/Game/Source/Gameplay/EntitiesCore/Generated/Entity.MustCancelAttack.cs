namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.MustCancelAttack MustCancelAttackC => GetComponent<Game.Gameplay.Features.Attack.MustCancelAttack>();

		public Game.Utility.Conditions.ICompositeCondition MustCancelAttack => MustCancelAttackC.Value;

		public bool TryGetMustCancelAttack(out Game.Utility.Conditions.ICompositeCondition value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Attack.MustCancelAttack component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Conditions.ICompositeCondition);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddMustCancelAttack(Game.Utility.Conditions.ICompositeCondition value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.MustCancelAttack() {Value = value});
		}

	}
}
