namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.StartAttackEvent StartAttackEventC => GetComponent<Game.Gameplay.Features.Attack.StartAttackEvent>();

		public Game.Utility.Reactive.ReactiveEvent StartAttackEvent => StartAttackEventC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddStartAttackEvent()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.StartAttackEvent() {Value = new Game.Utility.Reactive.ReactiveEvent() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddStartAttackEvent(Game.Utility.Reactive.ReactiveEvent value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.StartAttackEvent() {Value = value});
		}

	}
}
