namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.AttackDelayEndEvent AttackDelayEndEventC => GetComponent<Game.Gameplay.Features.Attack.AttackDelayEndEvent>();

		public Game.Utility.Reactive.ReactiveEvent AttackDelayEndEvent => AttackDelayEndEventC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddAttackDelayEndEvent()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AttackDelayEndEvent() {Value = new Game.Utility.Reactive.ReactiveEvent() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddAttackDelayEndEvent(Game.Utility.Reactive.ReactiveEvent value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AttackDelayEndEvent() {Value = value});
		}

	}
}
