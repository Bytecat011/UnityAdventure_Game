namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Sensors.IsTouchDeathMask IsTouchDeathMaskC => GetComponent<Game.Gameplay.Features.Sensors.IsTouchDeathMask>();

		public Game.Utility.Reactive.ReactiveVariable<System.Boolean> IsTouchDeathMask => IsTouchDeathMaskC.Value;

		public bool TryGetIsTouchDeathMask(out Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Sensors.IsTouchDeathMask component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Boolean>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddIsTouchDeathMask()
		{
			return AddComponent(new Game.Gameplay.Features.Sensors.IsTouchDeathMask() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Boolean>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddIsTouchDeathMask(Game.Utility.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new Game.Gameplay.Features.Sensors.IsTouchDeathMask() {Value = value});
		}

	}
}
