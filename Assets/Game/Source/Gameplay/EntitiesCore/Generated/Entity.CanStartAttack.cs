namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.CanStartAttack CanStartAttackC => GetComponent<Game.Gameplay.Features.Attack.CanStartAttack>();

		public Game.Utility.Conditions.ICompositeCondition CanStartAttack => CanStartAttackC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddCanStartAttack(Game.Utility.Conditions.ICompositeCondition value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.CanStartAttack() {Value = value});
		}

	}
}
