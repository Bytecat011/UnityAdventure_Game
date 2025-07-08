namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Sensors.ContactColliderBuffer ContactColliderBufferC => GetComponent<Game.Gameplay.Features.Sensors.ContactColliderBuffer>();

		public Game.Utility.Buffer<UnityEngine.Collider> ContactColliderBuffer => ContactColliderBufferC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddContactColliderBuffer(Game.Utility.Buffer<UnityEngine.Collider> value)
		{
			return AddComponent(new Game.Gameplay.Features.Sensors.ContactColliderBuffer() {Value = value});
		}

	}
}
