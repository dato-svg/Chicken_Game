using System;
using BaseScripts.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace BaseScripts
{
    [RequireComponent(typeof(Button))]
    public class UIButtonShop : MonoBehaviour
    {
        [SerializeField] private int priceBuy;
        [SerializeField] private int moneyForSec;
        [SerializeField] private ChickenConfig config;
        
        private FloatingTextSpawner _floatingTextSpawner;
        
        private Button _button;

        private void Awake() => 
            _button = GetComponent<Button>();

        private void Start()
        {
            _floatingTextSpawner = GameObject.Find("FloatingTextSpawner").GetComponent<FloatingTextSpawner>();
        }

        private void OnEnable() => 
            _button.onClick.AddListener(BuyUpgrade);

        private void OnDisable() => 
            _button.onClick.RemoveListener(BuyUpgrade);

        private void BuyUpgrade()
        {
            if (priceBuy <= MoneyManager.Instance.GetMoney())
            {
                MoneyManager.Instance.SpendMoney(priceBuy);
                _floatingTextSpawner.SpawnAndAnimate(0,$" -{priceBuy}");
                config.PassiveIncome += moneyForSec;
            }
        }
    }
}
