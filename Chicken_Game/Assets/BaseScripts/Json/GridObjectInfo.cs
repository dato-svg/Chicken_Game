using UnityEngine;

namespace BaseScripts.Json
{
    public class GridObjectInfo : MonoBehaviour
    {
        public string prefabName;


        private void Awake()
        {
            prefabName = gameObject.name + Random.Range(0, 999999).ToString();
        }
    }
}
