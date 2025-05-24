using System.Collections.Generic;
using UnityEngine;
using System.IO;
using BaseScripts.Json;

namespace BaseScripts.Test
{
    public class GridLoad : MonoBehaviour
    {
        /*
            [SerializeField] private List<GameObject> gridObjects = new List<GameObject>();

            [SerializeField] private List<GameObject> spawnablePrefabs;

            private string savePath => Application.persistentDataPath + "/grid_save.json";

            private void Start()
            {
                LoadGridObjects();
            }

            public void AddGridObject(GameObject gridObject)
            {
                gridObjects.Add(gridObject);
            }

            public List<GameObject> GetGridObjects()
            {
                return gridObjects;
            }

            public void SaveGridObjects()
            {
                List<GridObjectData> dataList = new List<GridObjectData>();

                foreach (var obj in gridObjects)
                {
                    var info = obj.GetComponent<GridObjectInfo>();
                    if (info == null)
                    {
                        Debug.LogWarning($"У объекта {obj.name} отсутствует компонент GridObjectInfo");
                        continue;
                    }

                    var data = new GridObjectData
                    {
                        prefabName = info.prefabName,
                        position = obj.transform.position
                    };

                    dataList.Add(data);
                }

                string json = JsonUtility.ToJson(new GridObjectDataWrapper { objects = dataList }, true);
                File.WriteAllText(savePath, json);
                Debug.Log("Сохранено: " + savePath);
            }

            public void LoadGridObjects()
            {
                if (!File.Exists(savePath))
                {
                    Debug.Log("Файл сохранения не найден.");
                    return;
                }

                string json = File.ReadAllText(savePath);
                var wrapper = JsonUtility.FromJson<GridObjectDataWrapper>(json);

                foreach (var data in wrapper.objects)
                {
                    GameObject prefab = spawnablePrefabs.Find(p => p.name == data.prefabName);

                    if (prefab != null)
                    {
                        GameObject obj = Instantiate(prefab, data.position, Quaternion.identity, transform);

                        var info = obj.GetComponent<GridObjectInfo>();
                        if (info == null)
                        {
                            info = obj.AddComponent<GridObjectInfo>();
                        }

                        info.prefabName = data.prefabName;

                        gridObjects.Add(obj);
                    }
                    else
                    {
                        Debug.LogWarning("Префаб не найден: " + data.prefabName);
                    }
                }

                Debug.Log("Объекты загружены");
            }

            [System.Serializable]
            private class GridObjectDataWrapper
            {
                public List<GridObjectData> objects;
            }
        }*/
    }
}