using UnityEngine;

namespace BaseScripts.DragAndDrop
{
    public class ChickenFactory : MonoBehaviour
    {
        [SerializeField] private GameObject[] chickenPrefabs;
        
        public GameObject CreateChicken(int level, Transform parent = null)
        {
            if (level - 1 >= chickenPrefabs.Length || level <= 0)
            {
                Debug.LogWarning("Недопустимый уровень курицы!");
                return null;
            }
            
            var chicken = Instantiate(chickenPrefabs[level - 1], parent);
            chicken.transform.localPosition = Vector3.zero;
            chicken.transform.localPosition = new Vector3(0, 52, 0);
            return chicken;
        }
    }
}
