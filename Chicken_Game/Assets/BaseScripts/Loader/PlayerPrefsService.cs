using UnityEngine;

namespace BaseScripts.Loader
{
    public class PlayerPrefsService : MonoBehaviour
    {
        public static PlayerPrefsService Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        
        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
        
        public int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
        
        public void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }
        
        public float LoadFloat(string key, float defaultValue = 0f)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }
        
        public void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        
        public string LoadString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }
        
        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
        
        public void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
        
        public void ClearAll()
        {
            PlayerPrefs.DeleteAll();
        }
        
        public void SaveObject<T>(string key, T obj)
        {
            string json = JsonUtility.ToJson(obj);
            SaveString(key, json);
        }
        
        public T LoadObject<T>(string key)
        {
            string json = LoadString(key);
            return JsonUtility.FromJson<T>(json);
        }
    }
}
