namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.StartAttackRequest StartAttackRequestC => GetComponent<Game.Gameplay.Features.Attack.StartAttackRequest>();

		public Game.Utility.Reactive.ReactiveEvent StartAttackRequest => StartAttackRequestC.Value;

		public bool TryGetStartAttackRequest(out Game.Utility.Reactive.ReactiveEvent value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Attack.StartAttackRequest component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveEvent);
			return result;
		}

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
