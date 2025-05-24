using BaseScripts.Loader;
using BaseScripts.Test;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BaseScripts.DragAndDrop
{
    [RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
    public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Image image;
        
        private Canvas _mainCanvas;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        
        private FloatingTextSpawner _floatingTextSpawner;
        
        private GridLoad _gridLoad;
        
        private void Start()
        {
            _floatingTextSpawner = GameObject.Find("FloatingTextSpawner").GetComponent<FloatingTextSpawner>();
            _gridLoad = GameObject.Find("ContentCurrent").GetComponent<GridLoad>();
            
            FindShop();
            
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _mainCanvas = GetComponentInParent<Canvas>();
        }
        
        private void FindShop()
        {
            if (image == null) 
            {
                GameObject shopObj = GameObject.Find("Shop");
                if (shopObj != null)
                {
                    image = shopObj.GetComponent<Image>();
                }
                else
                {
                    Debug.LogWarning("Объект Shop не найден, image остался null.");
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            var slotTransform = _rectTransform.parent;
            slotTransform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;

            if (image != null)
            { 
                image.GetComponent<Image>().enabled = false;
                image.GetComponent<Button>().enabled = false;
            }
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;

            if (image != null)
            {
                image.GetComponent<Image>().enabled = true;
                image.GetComponent<Button>().enabled = true;
            }
           
            var raycastResults = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);
            
            foreach (var result in raycastResults)
            {
                if (result.gameObject.CompareTag("SellZone"))
                {
                    Destroy(gameObject);
                    MoneyManager.Instance.AddMoney(30);
                    PlayerPrefsService.Instance.SaveInt("Money", MoneyManager.Instance.GetMoney());
                    _floatingTextSpawner.SpawnAndAnimate(1," +30");
                    Debug.Log("Курица продана!");
                    return;
                }
            }
            
            _rectTransform.localPosition = new Vector3(0, 52, 0);
        }
    }
}
