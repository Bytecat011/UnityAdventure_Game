namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Movement.CanMove CanMoveC => GetComponent<Game.Gameplay.Features.Movement.CanMove>();

		public Game.Utility.Conditions.ICompositeCondition CanMove => CanMoveC.Value;

		public bool TryGetCanMove(out Game.Utility.Conditions.ICompositeCondition value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Movement.CanMove component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Conditions.ICompositeCondition);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddCanMove(Game.Utility.Conditions.ICompositeCondition value)
		{
			return AddComponent(new Game.Gameplay.Features.Movement.CanMove() {Value = value});
		}

	}
}
