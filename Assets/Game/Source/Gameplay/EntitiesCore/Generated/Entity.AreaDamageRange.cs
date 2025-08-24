namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.AreaDamage.AreaDamageRange AreaDamageRangeC => GetComponent<Game.Gameplay.Features.Attack.AreaDamage.AreaDamageRange>();

		public Game.Utility.Reactive.ReactiveVariable<System.Single> AreaDamageRange => AreaDamageRangeC.Value;

		public bool TryGetAreaDamageRange(out Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Attack.AreaDamage.AreaDamageRange component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveVariable<System.Single>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddAreaDamageRange()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AreaDamage.AreaDamageRange() {Value = new Game.Utility.Reactive.ReactiveVariable<System.Single>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddAreaDamageRange(Game.Utility.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AreaDamage.AreaDamageRange() {Value = value});
		}

	}
}
