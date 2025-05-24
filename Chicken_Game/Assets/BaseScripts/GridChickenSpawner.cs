using BaseScripts.Test;
using UnityEngine;

namespace BaseScripts
{
    public class GridChickenSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject lockImage;

        [SerializeField] private GameObject spawnGrid;
        [SerializeField] private Transform  spawnParent;
        
        [SerializeField] private GridLoad gridLoad;
        
        private bool _canSpawn;

        private void Start()
        {
            spawnGrid = Resources.Load<GameObject>("Grid");
            gridLoad = GameObject.Find("ContentCurrent").GetComponent<GridLoad>();
            spawnParent = transform.parent.parent;
            Debug.Log("SpawnParentName" + spawnParent);
        }
        
        private void Update()
        {
            if (_canSpawn == false)
            {
                if (lockImage.activeSelf)
                {
                    GameObject grid = Instantiate(spawnGrid, spawnParent);
                  //  gridLoad.AddGridObject(grid);
                  //  gridLoad.SaveGridObjects();
                    _canSpawn = true;
                    return;
                } 
            }
            
        }
    }
}
