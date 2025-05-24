using System;
using BaseScripts.Loader;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BaseScripts
{
    public class FeatherController : MonoBehaviour,  IPointerClickHandler
    {
        [SerializeField] private float fallSpeed = 100f;
        [SerializeField] private float lifetime = 5f;

        private RectTransform _rectTransform;
        private float _timer;
        
        private void Awake() => 
            _rectTransform = GetComponent<RectTransform>();

      

        private void Update()
        {
            _rectTransform.anchoredPosition -= new Vector2(0f, fallSpeed * Time.deltaTime);
            
            _timer += Time.deltaTime;
            if (_timer >= lifetime) 
                Destroy(gameObject);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            MoneyManager.Instance.AddFeather(1);
            Destroy(gameObject);
        }
    }
}