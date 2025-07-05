namespace Game.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Game.Gameplay.Common.CharacterControllerComponent CharacterControllerC => GetComponent<Game.Gameplay.Common.CharacterControllerComponent>();

		public UnityEngine.CharacterController CharacterController => CharacterControllerC.Value;

		public Game.Gameplay.EntitiesCore.Entity AddCharacterController(UnityEngine.CharacterController value)
		{
			return AddComponent(new Game.Gameplay.Common.CharacterControllerComponent() {Value = value});
		}

	}
}
