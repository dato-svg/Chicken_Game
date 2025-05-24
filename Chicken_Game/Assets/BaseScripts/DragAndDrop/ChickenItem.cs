using System.Collections;
using BaseScripts.Configs;
using BaseScripts.Loader;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BaseScripts.DragAndDrop
{
    public class ChickenItem : MonoBehaviour, IPointerClickHandler
    {
        private static readonly int Enter = Animator.StringToHash("Enter");
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public Transform CurrentSlot { get; private set; }
        
        [SerializeField] private ChickenConfig config;
        [SerializeField] private GameObject addMoneyText;
        
        private GameObject _coinIcon;
        private Coroutine _moneyCoroutine;

        private void Awake()
        {
            _coinIcon = transform.GetChild(1).gameObject;
            _coinIcon.SetActive(false);
        }

        private void Start()
        {
            addMoneyText = Resources.Load<GameObject>("AddMoneyPrefab");
            StartGivingMoney();
            SetCurrentSlot(transform.parent);
        }
        
        public void Initialize(int level, Transform parentSlot)
        {
            Level = level;
            CurrentSlot = parentSlot;
            if (parentSlot != null) 
                parentSlot.GetComponentInParent<UISlot>().ShowLevel(Level);
        }
        
        public void SetCurrentSlot(Transform newSlot) => 
            CurrentSlot = newSlot;
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            MoneyManager.Instance.AddMoney(config.ClickIncome);

            PlayTextAnimation(config.ClickIncome);
            
            Debug.Log($" Клик по курице lvl {Level} дал {config.ClickIncome} монет");
            
            
            Debug.Log(MoneyManager.Instance.GetMoney());
        }
        
        private void StartGivingMoney()
        {
            if (_moneyCoroutine != null)
                StopCoroutine(_moneyCoroutine);
        
            _moneyCoroutine = StartCoroutine(GiveMoneyOverTime());
        }
        
        private IEnumerator GiveMoneyOverTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(2f); 
                MoneyManager.Instance.AddMoney(config.PassiveIncome);
                PlayMoneyAnimation();
                
                Debug.Log($"Курица lvl {Level} дала {config.PassiveIncome} монет (пассивно)");
            }
            // ReSharper disable once IteratorNeverReturns
        }
        
        private void PlayTextAnimation(int money)
        {
            if (addMoneyText == null) return;

            GameObject textObj = Instantiate(addMoneyText, GameObject.Find("Point").transform);
            RectTransform rectTransform = textObj.GetComponent<RectTransform>();
            TextMeshProUGUI tmpText = textObj.GetComponent<TextMeshProUGUI>();
            CanvasGroup canvasGroup = textObj.GetComponent<CanvasGroup>();

            if (canvasGroup == null)
                canvasGroup = textObj.AddComponent<CanvasGroup>();
            
            rectTransform.localPosition = Vector3.zero;
            tmpText.text =$"+ {money.ToString()}";
            canvasGroup.alpha = 1f;

            float moveDistance = 200f;
            float duration = 2f;

            
            Sequence seq = DOTween.Sequence();
            seq.Append(rectTransform.DOLocalMoveY(rectTransform.localPosition.y + moveDistance, duration).SetEase(Ease.OutCubic));
            seq.Join(canvasGroup.DOFade(0f, duration));
            seq.AppendCallback(() =>
            {
                Destroy(textObj);
            });
        }

        
        private void PlayMoneyAnimation()
        {
            float moveDistance = 150f;
            float duration = 1f;

            Transform iconTransform = _coinIcon.transform;
            Vector3 startPos = iconTransform.localPosition;
            
            iconTransform.DOKill();

            CanvasGroup canvasGroup = iconTransform.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = iconTransform.gameObject.AddComponent<CanvasGroup>();
            
            Sequence seq = DOTween.Sequence();
            seq.Append(iconTransform.DOLocalMoveY(startPos.y + moveDistance, duration).SetEase(Ease.OutQuad));
            _coinIcon.SetActive(true);
            seq.Join(canvasGroup.DOFade(0f, duration));
            seq.AppendCallback(() =>
            {
                iconTransform.localPosition = startPos;
                _coinIcon.SetActive(false);
                canvasGroup.alpha = 1f;
            });
        }

        
    }
}
