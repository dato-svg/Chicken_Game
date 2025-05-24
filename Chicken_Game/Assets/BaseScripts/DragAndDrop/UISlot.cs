using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BaseScripts.DragAndDrop
{
    public class UISlot : MonoBehaviour, IDropHandler
    {
        [field: SerializeField] public int Level { get; set; }

        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Image image;
        [SerializeField] private ActiveChickenAnimators activeChickenAnimators;

        private SlotLockController _slotLockController;
        private static readonly HashSet<int> ActivatedLevels = new();

        private void Start()
        {
            _slotLockController = GetComponentInParent<SlotLockController>();
            image = GameObject.Find("Shop").GetComponent<Image>();
            activeChickenAnimators = GameObject.Find("NewChickenAnimation").GetComponent<ActiveChickenAnimators>();

            activeChickenAnimators.transform.localScale = Vector3.zero;
            levelText.transform.parent.gameObject.SetActive(false);
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (_slotLockController != null && _slotLockController.isLocked)
                return;

            var draggedObject = eventData.pointerDrag;
            if (draggedObject == null) return;

            if (image != null)
                image.gameObject.SetActive(true);

            var draggedChicken = draggedObject.GetComponent<ChickenItem>();
            var draggedEgg = draggedObject.GetComponent<EggItem>();

            var targetChicken = GetComponentInChildren<ChickenItem>();
            var targetEgg = GetComponentInChildren<EggItem>();
            
            if ((draggedChicken != null && draggedChicken.CurrentSlot == transform) ||
                (draggedEgg != null && draggedEgg.CurrentSlot == transform))
            {
                return;
            }
            
            if (draggedEgg != null)
            {
                if (targetChicken == null && targetEgg == null)
                {
                    draggedObject.transform.SetParent(transform);
                    draggedObject.transform.localPosition = new Vector3(0, 52, 0);
                    draggedEgg.SetCurrentSlot(transform);
                }
                return;
            }
            
            if (draggedChicken != null)
            {
                if (draggedChicken.CurrentSlot != null)
                {
                    var oldSlotVisual = draggedChicken.CurrentSlot.GetComponentInChildren<TextMeshProUGUI>(true);
                    if (oldSlotVisual != null)
                        oldSlotVisual.transform.parent.gameObject.SetActive(false);
                }
                
                if (targetChicken == null && targetEgg == null)
                {
                    draggedObject.transform.SetParent(transform);
                    draggedObject.transform.localPosition = new Vector3(0, 52, 0);
                    draggedChicken.SetCurrentSlot(transform);
                    
                    var newSlotVisual = GetComponentInChildren<TextMeshProUGUI>(true);
                    if (newSlotVisual != null)
                        newSlotVisual.transform.parent.gameObject.SetActive(true);
                    
                    ShowLevel(draggedChicken.Level);
                    return;
                }
                    
                if (targetChicken != null && draggedChicken.Level == targetChicken.Level && targetEgg == null)
                {
                    Destroy(draggedObject.gameObject);
                    Destroy(targetChicken.gameObject);
                    
                    int newLevel = draggedChicken.Level + 1;
                    
                    if (!ActivatedLevels.Contains(draggedChicken.Level))
                    {
                        activeChickenAnimators.transform.localScale = Vector3.one;
                        activeChickenAnimators.AnimateNextChicken();
                        ActivatedLevels.Add(draggedChicken.Level);
                    }
                    
                    ChickenMergeManager.Instance.SpawnChicken(newLevel, transform);
                    ShowLevel(newLevel);
                }
            }
        }

        public void ShowLevel(int level)
        {
            Level = level;
            levelText.text = Level.ToString();
        }
    }
}
