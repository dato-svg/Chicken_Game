using System.Collections;
using BaseScripts.DragAndDrop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaseScripts
{
    public class ChickenButtonSpawner : MonoBehaviour
    {
        [SerializeField] private Button spawnButton;
        [SerializeField] private string slotTag = "Slot";
        [SerializeField] private int chickenPrice;
        [SerializeField] private float autoSpawnInterval = 15f;
        [SerializeField] private GameObject eggPrefab;
        
        [SerializeField] private GameObject removeMoneyText;
        [SerializeField] private Transform parentUI;
        
        private void Start()
        {
            spawnButton.onClick.AddListener(SpawnInFirstFreeSlot);
            StartCoroutine(AutoSpawnRoutine());
        }
        
        private void OnDisable()
        {
            spawnButton.onClick.RemoveListener(SpawnInFirstFreeSlot);
            StopAllCoroutines();
        }
        
        private void SpawnInFirstFreeSlot()
        {
            var allSlots = GameObject.FindGameObjectsWithTag(slotTag);
            
            foreach (var slot in allSlots)
            {
                var chicken = slot.GetComponentInChildren<ChickenItem>();
                var egg = slot.GetComponentInChildren<EggItem>();
                var slotLock = slot.GetComponent<SlotLockController>();
               
                if (chicken == null && egg == null && slotLock.isLocked == false)
                {
                    if (MoneyManager.Instance.GetMoney() >= chickenPrice)
                    {
                        MoneyManager.Instance.SpendMoney(chickenPrice);
                        ShowRemoveMoneyText(parentUI);
                        ChickenMergeManager.Instance.SpawnChicken(1,slot.transform);
                        return;
                    }
                }
            }
            Debug.Log("Нет свободных слотов для спавна курицы!");
        }
        
        private IEnumerator AutoSpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(autoSpawnInterval);
                TrySpawnChicken();
            }
            // ReSharper disable once IteratorNeverReturns
        }
        
        private bool TrySpawnChicken()
        {
            var allSlots = GameObject.FindGameObjectsWithTag(slotTag);

            foreach (var slot in allSlots)
            {
                var chicken = slot.GetComponentInChildren<ChickenItem>();
                var egg = slot.GetComponentInChildren<EggItem>();
                var slotLock = slot.GetComponent<SlotLockController>();
                
                if (chicken == null && egg == null && slotLock.isLocked == false)
                {
                   GameObject newEgg = Instantiate(eggPrefab, slot.transform.position, Quaternion.identity, slot.transform);
                   newEgg.transform.localPosition = new Vector3(slot.transform.position.x, 20, slot.transform.position.z);
                   Debug.Log("IA ZASPAVNIL EGG"); 
                   return true;
                }
            }

            Debug.Log("Нет свободных слотов или недостаточно денег для спавна курицы!");
            return false;
        }
        
        private void ShowRemoveMoneyText(Transform parent)
        {
            GameObject textObj = Instantiate(removeMoneyText, parent);
            textObj.GetComponent<TextMeshProUGUI>().text = $" -{chickenPrice.ToString()}";
            RectTransform rect = textObj.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;

            CanvasGroup canvasGroup = textObj.AddComponent<CanvasGroup>();
            StartCoroutine(AnimateAndDestroy(textObj, rect, canvasGroup));
        }
        
        private IEnumerator AnimateAndDestroy(GameObject obj, RectTransform rect, CanvasGroup canvasGroup)
        {
            float duration = 1.2f;
            float elapsed = 0f;

            Vector2 startPos = rect.anchoredPosition;
            Vector2 targetPos = startPos + new Vector2(0, 100);

            while (elapsed < duration)
            {
                float t = elapsed / duration;

                rect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);

                elapsed += Time.deltaTime;
                yield return null;
            }

            Destroy(obj);
        }
    }
}
