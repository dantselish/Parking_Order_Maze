public class AchievementsManager
{
    public int movesMade { get; private set; }
    public int carsCrashed { get; private set; }


    public AchievementsManager(int movesMade, int carsCrashed)
    {
        this.movesMade = movesMade;
        this.carsCrashed = carsCrashed;
    }

    public void AddMoveMade()
    {
        ++movesMade;
    }

    public void AddCarsCrashed()
    {
        ++carsCrashed;
    }
}
