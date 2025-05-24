using UnityEngine;
using UnityEngine.UI;

namespace BaseScripts
{
    public class PlaceLockController : MonoBehaviour
    {
        [SerializeField] private GameObject lockImage;
        [SerializeField] private GameObject firstActiveSlot;

        public int featherPrice;
        
        private Button _button;        

        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(UnlockSlot);
        }
        
        private void UnlockSlot()
        {
            if (MoneyManager.Instance.GetFeather() >= featherPrice)
            {
                MoneyManager.Instance.SpendFeather(featherPrice);

                lockImage.SetActive(false);
                firstActiveSlot.SetActive(true);

                _button.interactable = false;
            }
            else
            {
                Debug.Log("Недостаточно перьев для разблокировки!");
            }
        }
        
    }
}
