namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.AttackDelayTime AttackDelayTimeC => GetComponent<Game.Gameplay.Features.Attack.AttackDelayTime>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> AttackDelayTime => AttackDelayTimeC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddAttackDelayTime()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AttackDelayTime() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddAttackDelayTime(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AttackDelayTime() {Value = value});
		}

	}
}
