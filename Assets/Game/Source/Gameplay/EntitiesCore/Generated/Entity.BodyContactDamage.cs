namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.ContactTakeDamage.BodyContactDamage BodyContactDamageC => GetComponent<Game.Gameplay.Features.ContactTakeDamage.BodyContactDamage>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> BodyContactDamage => BodyContactDamageC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddBodyContactDamage()
		{
			return AddComponent(new Game.Gameplay.Features.ContactTakeDamage.BodyContactDamage() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddBodyContactDamage(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.ContactTakeDamage.BodyContactDamage() {Value = value});
		}

	}
}
