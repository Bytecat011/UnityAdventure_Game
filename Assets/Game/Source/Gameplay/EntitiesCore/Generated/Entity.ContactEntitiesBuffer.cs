namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Sensors.ContactEntitiesBuffer ContactEntitiesBufferC => GetComponent<Game.Gameplay.Features.Sensors.ContactEntitiesBuffer>();

		public Game.Utility.Buffer<Game.Gameplay.EntitiesCore.Entity> ContactEntitiesBuffer => ContactEntitiesBufferC.Value;

		public bool TryGetContactEntitiesBuffer(out Game.Utility.Buffer<Game.Gameplay.EntitiesCore.Entity> value)
		{
			bool result = TryGetComponent(out Game.Gameplay.Features.Sensors.ContactEntitiesBuffer component);
			if(result)
				value = component.Value;
			else
				value = default(Game.Utility.Buffer<Game.Gameplay.EntitiesCore.Entity>);
			return result;
		}

		public Game.Gameplay.EntitiesCore.Entity AddContactEntitiesBuffer(Game.Utility.Buffer<Game.Gameplay.EntitiesCore.Entity> value)
		{
			return AddComponent(new Game.Gameplay.Features.Sensors.ContactEntitiesBuffer() {Value = value});
		}

	}
}
