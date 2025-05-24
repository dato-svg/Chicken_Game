using UnityEngine;

namespace BaseScripts.DragAndDrop
{
    public class ChickenMergeManager : MonoBehaviour
    {
        public static ChickenMergeManager Instance;
        
        [SerializeField] private ChickenFactory chickenFactory;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }
        
        public void SpawnChicken(int level, Transform parent = null)
        {
            if (parent == null)
            {
                Debug.LogWarning("Parent is null in SpawnChicken!");
                return;
            }
            
            var newChicken = chickenFactory.CreateChicken(level, parent);

            newChicken.transform.localPosition = new Vector3(0, 52, 0);
            
            var chickenItem = newChicken.GetComponent<ChickenItem>();
            chickenItem.Initialize(level, parent);
            if (parent != null) parent.GetChild(0).gameObject.SetActive(true);
        }

    }
}


