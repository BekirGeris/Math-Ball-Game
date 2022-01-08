public class HighScore
{
    public string uuid;

    public string highScoreName;

    public int highScore;

    public HighScore(string uuid, string highScoreName, int highScore)
    {
        this.uuid = uuid;
        this.highScoreName = highScoreName;
        this.highScore = highScore;
    }
}

