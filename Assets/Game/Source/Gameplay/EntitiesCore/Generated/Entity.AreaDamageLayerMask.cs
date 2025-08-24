namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.AreaDamage.AreaDamageLayerMask AreaDamageLayerMaskC => GetComponent<Game.Gameplay.Features.Attack.AreaDamage.AreaDamageLayerMask>();

		public UnityEngine.LayerMask AreaDamageLayerMask => AreaDamageLayerMaskC.Value;

		public bool TryGetAreaDamageLayerMask(out UnityEngine.LayerMask value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Attack.AreaDamage.AreaDamageLayerMask component);
			if(result)
				value = component.Value;
			else
				value = default(UnityEngine.LayerMask);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddAreaDamageLayerMask(UnityEngine.LayerMask value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AreaDamage.AreaDamageLayerMask() {Value = value});
		}

	}
}
