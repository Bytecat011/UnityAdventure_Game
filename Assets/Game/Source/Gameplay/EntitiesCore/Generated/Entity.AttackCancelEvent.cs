namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.AttackCancelEvent AttackCancelEventC => GetComponent<Game.Gameplay.Features.Attack.AttackCancelEvent>();

		public Game.Utility.Reactive.ReactiveEvent AttackCancelEvent => AttackCancelEventC.Value;

		public bool TryGetAttackCancelEvent(out Game.Utility.Reactive.ReactiveEvent value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Attack.AttackCancelEvent component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveEvent);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddAttackCancelEvent()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AttackCancelEvent() {Value = new Game.Utility.Reactive.ReactiveEvent() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddAttackCancelEvent(Game.Utility.Reactive.ReactiveEvent value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AttackCancelEvent() {Value = value});
		}

	}
}
