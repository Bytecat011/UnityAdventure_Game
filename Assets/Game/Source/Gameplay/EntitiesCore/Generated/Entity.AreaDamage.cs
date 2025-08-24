namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.AreaDamage.AreaDamage AreaDamageC => GetComponent<Game.Gameplay.Features.Attack.AreaDamage.AreaDamage>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> AreaDamage => AreaDamageC.Value;

		public bool TryGetAreaDamage(out Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Attack.AreaDamage.AreaDamage component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddAreaDamage()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AreaDamage.AreaDamage() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddAreaDamage(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AreaDamage.AreaDamage() {Value = value});
		}

	}
}
