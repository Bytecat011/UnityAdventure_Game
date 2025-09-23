namespace Game.Gameplay.Features.Abilities
{
    public abstract class Ability
    {
        protected Ability(string id)
        {
            ID = id;
        }
        
        public string ID { get; }

        public abstract void Activate();
    }
}