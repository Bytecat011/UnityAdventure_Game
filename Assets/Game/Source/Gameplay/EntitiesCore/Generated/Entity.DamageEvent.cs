namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.ApplyDamage.DamageEvent DamageEventC => GetComponent<Game.Gameplay.Features.ApplyDamage.DamageEvent>();

		public Game.Utility.Reactive.ReactiveEvent<System.Single> DamageEvent => DamageEventC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddDamageEvent()
		{
			return AddComponent(new Game.Gameplay.Features.ApplyDamage.DamageEvent() {Value = new Game.Utility.Reactive.ReactiveEvent<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddDamageEvent(Game.Utility.Reactive.ReactiveEvent<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.ApplyDamage.DamageEvent() {Value = value});
		}

	}
}
