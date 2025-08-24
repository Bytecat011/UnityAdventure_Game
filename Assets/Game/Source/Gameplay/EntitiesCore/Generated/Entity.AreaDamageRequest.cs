namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.AreaDamage.AreaDamageRequest AreaDamageRequestC => GetComponent<Game.Gameplay.Features.Attack.AreaDamage.AreaDamageRequest>();

		public Game.Utility.Reactive.ReactiveEvent<UnityEngine.Vector3> AreaDamageRequest => AreaDamageRequestC.Value;

		public bool TryGetAreaDamageRequest(out Game.Utility.Reactive.ReactiveEvent<UnityEngine.Vector3> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Attack.AreaDamage.AreaDamageRequest component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Reactive.ReactiveEvent<UnityEngine.Vector3>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddAreaDamageRequest()
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AreaDamage.AreaDamageRequest() {Value = new Game.Utility.Reactive.ReactiveEvent<UnityEngine.Vector3>() });
		}

		public Game.Gameplay.EntitiesCore.Entity AddAreaDamageRequest(Game.Utility.Reactive.ReactiveEvent<UnityEngine.Vector3> value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AreaDamage.AreaDamageRequest() {Value = value});
		}

	}
}
