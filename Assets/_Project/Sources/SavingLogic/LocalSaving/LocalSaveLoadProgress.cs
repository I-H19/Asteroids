using UnityEngine;

namespace Asteroids
{
    public class LocalSaveLoadProgress : ISaveLoadProgress
    {
        public void SaveProgress(SaveData saveData)
        {
            string json = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString("SaveData", json);
        }

        public SaveData LoadProgress()
        {
            bool hasSaveData = PlayerPrefs.HasKey("SaveData");

            SaveData saveData;
            if (hasSaveData)
            {
                string json = PlayerPrefs.GetString("SaveData");
                saveData = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                saveData = new();
            }

            return saveData;
        }
    }
}