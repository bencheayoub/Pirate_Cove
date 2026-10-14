public enum Difficulty { Easy, Normal, Hard }

public static class GameSettings
{
    public static Difficulty Current = Difficulty.Normal;

    public static int StartLevel
    {
        get { return Current == Difficulty.Hard ? 3 : 1; }
    }

    public static float SecondsPerLevel
    {
        get
        {
            if (Current == Difficulty.Easy) return 20f;
            if (Current == Difficulty.Hard) return 8f;
            return 12f;
        }
    }
}
