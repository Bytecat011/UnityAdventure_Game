namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.StartAttackRequest StartAttackRequestC => GetComponent<Game.Gameplay.Features.Attack.StartAttackRequest>();

		public Game.Utility.Reactive.ReactiveEvent StartAttackRequest => StartAttackRequestC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddStartAttackRequest()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.StartAttackRequest() {Value = new Game.Utility.Reactive.ReactiveEvent() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddStartAttackRequest(Game.Utility.Reactive.ReactiveEvent value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.StartAttackRequest() {Value = value});
		}

	}
}
