using Game.Core.DI;
using Game.Utility.CoroutineManagement;

namespace Game.Utility.Timer
{
    public class TimerServiceFactory
    {
        private readonly DIContainer _container;

        public TimerServiceFactory(DIContainer container)
        {
            _container = container;
        }
        
        public TimerService Create(float cooldown)
            => new TimerService(cooldown, _container.Resolve<ICoroutineRunner>());
    }
}