namespace Game.Gameplay.EntitiesCore.Systems
{
    public interface IInitializableSystem : IEntitySystem
    {
        void OnInit(Entity entity);
    }
}