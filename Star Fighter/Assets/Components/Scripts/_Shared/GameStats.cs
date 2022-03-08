public sealed class GameStats
{
    private static GameStats instance = null;
    private static readonly object padlock = new object();

    public int Score { get; set; }

    public int Lives { get; set; }

    public int MAX_LIVES { get { return 3; } }

    public float RECOMMENDED_SHAKE_DURATION { get { return 0.25f; } }

    public int MINIMUM_NAME_LENGTH { get { return 3; } }

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