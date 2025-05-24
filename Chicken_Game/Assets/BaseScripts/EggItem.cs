using BaseScripts.DragAndDrop;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BaseScripts
{
    public class EggItem : MonoBehaviour, IPointerClickHandler
    {
        public Transform CurrentSlot { get; private set; }
        public int Level;

        public void SetCurrentSlot(Transform slot)
        {
            CurrentSlot = slot;
        }
        
        private void Start()
        {
            SetCurrentSlot(transform.parent); 
        }
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Transform slot = transform.parent;
            
            Destroy(gameObject);
            
            ChickenMergeManager.Instance.SpawnChicken(1, slot);
        }
    }
}