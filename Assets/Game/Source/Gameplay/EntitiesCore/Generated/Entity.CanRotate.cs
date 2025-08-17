namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Movement.CanRotate CanRotateC => GetComponent<Game.Gameplay.Features.Movement.CanRotate>();

		public Game.Utility.Conditions.ICompositeCondition CanRotate => CanRotateC.Value;

		public bool TryGetCanRotate(out Game.Utility.Conditions.ICompositeCondition value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Movement.CanRotate component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Conditions.ICompositeCondition);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddCanRotate(Game.Utility.Conditions.ICompositeCondition value)
		{
			return AddComponent(new Game.Gameplay.Features.Movement.CanRotate() {Value = value});
		}

	}
}
