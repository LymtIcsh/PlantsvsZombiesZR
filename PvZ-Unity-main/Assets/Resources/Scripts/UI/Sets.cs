
[System.Serializable]
public class LevelStatus
{
    public int levelNumber;
    public bool isCompleted;

    public LevelStatus(int levelNumber, bool isCompleted)
    {
        this.levelNumber = levelNumber;
        this.isCompleted = isCompleted;
    }
}
