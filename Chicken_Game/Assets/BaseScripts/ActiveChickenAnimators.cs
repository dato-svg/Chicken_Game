using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace BaseScripts
{
    public class ActiveChickenAnimators : MonoBehaviour
    {
        [SerializeField] private Sprite[] activeChicken;
        [SerializeField] private Sprite[] deactivateChicken;
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private Vector3 targetScale = Vector3.one;
        
        [SerializeField] private Image currentChickenImage;
        [SerializeField] private TextMeshProUGUI chickenLevelText;
        
        private int _currentIndex = 1;
        
        private Button _button;
        
        private void Start()
        {
            _currentIndex = 1;
            _button = GetComponentInChildren<Button>();
            _button.onClick.AddListener(DeactivatePanel);
        }
         
        private void DeactivatePanel() => 
            transform.localScale = Vector3.zero;
        
        
        public void AnimateNextChicken()
        {
            _button.interactable = false;
            int chickenLevel = _currentIndex + 1;
            chickenLevelText.text = chickenLevel.ToString();
            if (deactivateChicken.Length == 0 || activeChicken.Length == 0)
                return;
            
            if (_currentIndex >= deactivateChicken.Length || _currentIndex >= activeChicken.Length)
                _currentIndex = 0;
            
            int indexToUse = _currentIndex;
            
            currentChickenImage.sprite = deactivateChicken[indexToUse];
            currentChickenImage.transform.localScale = Vector3.zero;
            
            currentChickenImage.transform.DOScale(targetScale, animationDuration)
                .OnComplete(() =>
                {
                    currentChickenImage.sprite = activeChicken[indexToUse];
                    _button.interactable = true;
                });

            _currentIndex++;
            if (_currentIndex >= deactivateChicken.Length)
                _currentIndex = 0;
        }

    }
}