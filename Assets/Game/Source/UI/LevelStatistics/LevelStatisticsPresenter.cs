using Game.Meta.Features.LevelStatistics;
using Game.UI.Core;
using Game.Utility.Reactive;

namespace Game.UI.LevelStatistics
{
    public class LevelStatisticsPresenter : IPresenter
    {
        private readonly LevelStatisticsService _levelStatisticsService;
        private readonly LevelStatisticsView _view;

        private ISubscription _winSubscription;
        private ISubscription _loseSubscription;
        
        public LevelStatisticsPresenter(
            LevelStatisticsService levelStatisticsService,
            LevelStatisticsView view)
        {
            _levelStatisticsService = levelStatisticsService;
            _view = view;
        }

        public void Initialize()
        {
            _winSubscription = _levelStatisticsService.WonCount.Subscribe((_, _) => UpdateValues());
            _loseSubscription = _levelStatisticsService.LostCount.Subscribe((_, _) => UpdateValues());
            
            UpdateValues();
        }

        public void Dispose()
        {
            _winSubscription.Unsubscribe();
            _loseSubscription.Unsubscribe();
        }

        private void UpdateValues()
        {
            _view.SetWin(_levelStatisticsService.WonCount.Value);
            _view.SetLose(_levelStatisticsService.LostCount.Value);
        }
    }
}