namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Attack.AreaDamage.AreaDamageLayerMask AreaDamageLayerMaskC => GetComponent<Game.Gameplay.Features.Attack.AreaDamage.AreaDamageLayerMask>();

		public UnityEngine.LayerMask AreaDamageLayerMask => AreaDamageLayerMaskC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddAreaDamageLayerMask(UnityEngine.LayerMask value)
		{
			return AddComponent(new Game.Gameplay.Features.Attack.AreaDamage.AreaDamageLayerMask() {Value = value});
		}

	}
}
