namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.ApplyDamage.CanApplyDamage CanApplyDamageC => GetComponent<Game.Gameplay.Features.ApplyDamage.CanApplyDamage>();

		public Game.Utility.Conditions.ICompositeCondition CanApplyDamage => CanApplyDamageC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddCanApplyDamage(Game.Utility.Conditions.ICompositeCondition value)
		{
			return AddComponent(new Game.Gameplay.Features.ApplyDamage.CanApplyDamage() {Value = value});
		}

	}
}
