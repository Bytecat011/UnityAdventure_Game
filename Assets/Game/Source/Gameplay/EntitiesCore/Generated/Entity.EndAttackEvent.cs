namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.EndAttackEvent EndAttackEventC => GetComponent<Game.Gameplay.Features.Attack.EndAttackEvent>();

		public Game.Utility.Reactive.ReactiveEvent EndAttackEvent => EndAttackEventC.Value;

		public bool TryGetEndAttackEvent(out Game.Utility.Reactive.ReactiveEvent value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Attack.EndAttackEvent component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveEvent);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddEndAttackEvent()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.EndAttackEvent() {Value = new Game.Utility.Reactive.ReactiveEvent() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddEndAttackEvent(Game.Utility.Reactive.ReactiveEvent value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.EndAttackEvent() {Value = value});
		}

	}
}
