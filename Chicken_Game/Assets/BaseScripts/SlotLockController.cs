using BaseScripts.DragAndDrop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaseScripts
{
    public class SlotLockController : MonoBehaviour
    {
        [SerializeField] private GameObject lockImage;
        [SerializeField] private GameObject levelImage;
        
        [SerializeField] private GameObject nextActiveSlot;
        [SerializeField] private TextMeshProUGUI slotPriceText;
        
        [SerializeField] private int slotPrice;
        [SerializeField] public bool isLocked;
        
        private FloatingTextSpawner _floatingTextSpawner;
        
        private Button _button;
        
        
        private void Awake()
        {
            _floatingTextSpawner = GameObject.Find("FloatingTextSpawner").GetComponent<FloatingTextSpawner>();
            DeactivateAll();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(UnLockSlot);
            slotPriceText = transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        private void Start() => 
            slotPriceText.text = slotPrice.ToString();


        private void OnDisable() => 
            _button.onClick.RemoveListener(UnLockSlot);

        private void UnLockSlot()
        {
            if (MoneyManager.Instance.GetMoney() >= slotPrice)
            {
                MoneyManager.Instance.SpendMoney(slotPrice);

                _floatingTextSpawner.SpawnAndAnimate(0,$" -{slotPrice}");
                
                lockImage.SetActive(false);
                levelImage.SetActive(true);
                isLocked = false;
                _button.interactable = false;

                if (nextActiveSlot != null) 
                    nextActiveSlot.SetActive(true);
            }
            else
            {
                Debug.Log("Недостаточно денег для разблокировки!");
            }
        }
        
        private void DeactivateAll()
        {
            lockImage.SetActive(true);
            levelImage.SetActive(false);
            isLocked = true;
        }
    }
}
