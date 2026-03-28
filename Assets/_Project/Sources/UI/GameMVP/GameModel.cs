namespace _Project.Sources.UI.GameMVP
{
    public class Model
    {
        public int Score { get; private set; } = 0;
        public PlayerStats PlayerStats { get; private set; }

        public void ScoreIncrement() => Score++;
        public void ResetScore() => Score = 0;
        
        public Model(PlayerStats playerStats)
        {
            PlayerStats = playerStats;
        }
    }
}