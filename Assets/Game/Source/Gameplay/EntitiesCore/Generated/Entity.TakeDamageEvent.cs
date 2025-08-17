namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.ApplyDamage.TakeDamageEvent TakeDamageEventC => GetComponent<Game.Gameplay.Features.ApplyDamage.TakeDamageEvent>();

		public Game.Utility.Reactive.ReactiveEvent<System.Single> TakeDamageEvent => TakeDamageEventC.Value;

		public bool TryGetTakeDamageEvent(out Game.Utility.Reactive.ReactiveEvent<System.Single> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.ApplyDamage.TakeDamageEvent component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveEvent<System.Single>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddTakeDamageEvent()
		{
			return AddComponent(new Game.Gameplay.Features.ApplyDamage.TakeDamageEvent() {Value = new Game.Utility.Reactive.ReactiveEvent<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddTakeDamageEvent(Game.Utility.Reactive.ReactiveEvent<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.ApplyDamage.TakeDamageEvent() {Value = value});
		}

	}
}
