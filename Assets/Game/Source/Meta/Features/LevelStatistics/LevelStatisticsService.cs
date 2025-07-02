using Game.Data;
using Game.Utility.DataManagement.DataProviders;
using Game.Utility.Reactive;

namespace Game.Meta.Features.LevelStatistics
{
    public class LevelStatisticsService : IDataWriter<PlayerData>, IDataReader<PlayerData>
    {
        private readonly ReactiveVariable<int> _wonCount;
        private readonly ReactiveVariable<int> _lostCount;
        
        public IReactiveVariable<int> WonCount => _wonCount;
        public IReactiveVariable<int> LostCount => _lostCount;

        public LevelStatisticsService(PlayerDataProvider playerDataProvider)
        {
            _wonCount = new ReactiveVariable<int>(0);
            _lostCount = new ReactiveVariable<int>(0);
            
            playerDataProvider.RegisterReader(this);
            playerDataProvider.RegisterWriter(this);
        }

        public void HandleLevelResult(bool won)
        {
            if (won)
                _wonCount.Value++;
            else
                _lostCount.Value++;
        }

        public void Reset()
        {
            _wonCount.Value = 0;
            _lostCount.Value = 0;
        }

        public void WriteTo(PlayerData data)
        {
            data.LevelsStatistics.WinCount = _wonCount.Value;
            data.LevelsStatistics.LoseCount = _lostCount.Value;
        }

        public void ReadFrom(PlayerData data)
        {
            _wonCount.Value = data.LevelsStatistics.WinCount;
            _lostCount.Value = data.LevelsStatistics.LoseCount;
        }
    }
}