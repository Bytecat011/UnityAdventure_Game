namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Sensors.ContactColliderBuffer ContactColliderBufferC => GetComponent<Game.Gameplay.Features.Sensors.ContactColliderBuffer>();

		public Game.Utility.Buffer<UnityEngine.Collider> ContactColliderBuffer => ContactColliderBufferC.Value;

		public bool TryGetContactColliderBuffer(out Game.Utility.Buffer<UnityEngine.Collider> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Sensors.ContactColliderBuffer component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Buffer<UnityEngine.Collider>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddContactColliderBuffer(Game.Utility.Buffer<UnityEngine.Collider> value)
		{
			return AddComponent(new Game.Gameplay.Features.Sensors.ContactColliderBuffer() {Value = value});
		}

	}
}
