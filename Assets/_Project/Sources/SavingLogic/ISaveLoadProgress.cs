namespace Asteroids
{
    public interface ISaveLoadProgress
    {
        public void SaveProgress(SaveData saveData);
        public SaveData LoadProgress();
    }
}