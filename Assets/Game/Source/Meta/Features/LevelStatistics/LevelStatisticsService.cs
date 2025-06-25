using Game.Data;
using Game.Utility.DataManagement.DataProviders;

namespace Game.Meta.Features.LevelStatistics
{
    public class LevelStatisticsService : IDataWriter<PlayerData>, IDataReader<PlayerData>
    {
        private int wonCount = 0;
        private int lostCount = 0;
        
        public int WonCount => wonCount;
        public int LostCount => lostCount;

        public LevelStatisticsService(PlayerDataProvider playerDataProvider)
        {
            playerDataProvider.RegisterReader(this);
            playerDataProvider.RegisterWriter(this);
        }

        public void HandleLevelResult(bool won)
        {
            if (won)
                wonCount++;
            else
                lostCount++;
        }

        public void Reset()
        {
            wonCount = 0;
            lostCount = 0;
        }

        public void WriteTo(PlayerData data)
        {
            data.LevelsStatistics.WinCount = wonCount;
            data.LevelsStatistics.LoseCount = lostCount;
        }

        public void ReadFrom(PlayerData data)
        {
            wonCount = data.LevelsStatistics.WinCount;
            lostCount = data.LevelsStatistics.LoseCount;
        }
    }
}