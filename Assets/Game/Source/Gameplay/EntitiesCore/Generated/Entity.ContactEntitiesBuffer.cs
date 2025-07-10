namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Features.Sensors.ContactEntitiesBuffer ContactEntitiesBufferC => GetComponent<Game.Gameplay.Features.Sensors.ContactEntitiesBuffer>();

		public Game.Utility.Buffer<Game.Gameplay.EntitiesCore.Entity> ContactEntitiesBuffer => ContactEntitiesBufferC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddContactEntitiesBuffer(Game.Utility.Buffer<Game.Gameplay.EntitiesCore.Entity> value)
		{
			return AddComponent(new Game.Gameplay.Features.Sensors.ContactEntitiesBuffer() {Value = value});
		}

	}
}
