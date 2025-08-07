namespace Game.Providers
{
    public class GameDataProvider
    {
        public int CurrentLevelId { get; private set; }

        public GameDataProvider() { }

        public void ChangeLevelId(int levelId)
        {
            CurrentLevelId = levelId;
        }
    }
}

