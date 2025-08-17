namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.ApplyDamage.TakeDamageRequest TakeDamageRequestC => GetComponent<Game.Gameplay.Features.ApplyDamage.TakeDamageRequest>();

		public Game.Utility.Reactive.ReactiveEvent<System.Single> TakeDamageRequest => TakeDamageRequestC.Value;

		public bool TryGetTakeDamageRequest(out Game.Utility.Reactive.ReactiveEvent<System.Single> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.ApplyDamage.TakeDamageRequest component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveEvent<System.Single>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddTakeDamageRequest()
		{
			return AddComponent(new Game.Gameplay.Features.ApplyDamage.TakeDamageRequest() {Value = new Game.Utility.Reactive.ReactiveEvent<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddTakeDamageRequest(Game.Utility.Reactive.ReactiveEvent<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.ApplyDamage.TakeDamageRequest() {Value = value});
		}

	}
}
