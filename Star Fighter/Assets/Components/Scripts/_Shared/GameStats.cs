public sealed class GameStats
{
    private static GameStats instance = null;
    private static readonly object padlock = new object();

    public int Score { get; set; }

    public int Lives { get; set; }

    GameStats()
    {
    }

    public static GameStats Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new GameStats();
                }
                return instance;
            }
        }
    }
}