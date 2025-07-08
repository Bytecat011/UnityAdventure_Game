namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.ApplyDamage.DamageRequest DamageRequestC => GetComponent<Game.Gameplay.Features.ApplyDamage.DamageRequest>();

		public Game.Utility.Reactive.ReactiveEvent<System.Single> DamageRequest => DamageRequestC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddDamageRequest()
		{
			return AddComponent(new Game.Gameplay.Features.ApplyDamage.DamageRequest() {Value = new Game.Utility.Reactive.ReactiveEvent<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddDamageRequest(Game.Utility.Reactive.ReactiveEvent<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.ApplyDamage.DamageRequest() {Value = value});
		}

	}
}
