using System;
using BaseScripts.Loader;
using TMPro;
using UnityEngine;

namespace BaseScripts
{
    public class MoneyManager : MonoBehaviour
    {
        public static MoneyManager Instance { get; private set; }
        
        public int currentMoneyCoin = 0;
        [SerializeField] private TextMeshProUGUI moneyText;
        
        public int currentMoneyFeather;
        [SerializeField] private TextMeshProUGUI currentMoneyFeatherText;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }
        
        private void Start()
        {
            /*currentMoneyCoin = PlayerPrefsService.Instance.LoadInt("Money");
            currentMoneyFeather = PlayerPrefsService.Instance.LoadInt("Feather");

            if (currentMoneyCoin == 0) currentMoneyCoin = 500;
            
            if (currentMoneyFeather == 0) currentMoneyFeather = 10;*/
            
            UpdateUI();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentMoneyCoin += 50;
                currentMoneyFeather += 50;
                UpdateUI();
            }
        }
        
        public void AddMoney(int amount)
        {
            currentMoneyCoin += amount;
            UpdateUI();
        }
        
        public void SpendMoney(int amount)
        {
            currentMoneyCoin -= amount;
            UpdateUI();
        }
        
        public void AddFeather(int amount)
        {
            currentMoneyFeather += amount;
            UpdateUI();
        }
        
        public void SpendFeather(int amount)
        {
            currentMoneyFeather -= amount;
            UpdateUI();
        }
        
        public int GetMoney() => 
            currentMoneyCoin;
        
        public int GetFeather() => 
            currentMoneyFeather;
        
        private void UpdateUI()
        {
            if (moneyText != null)
                moneyText.text = $"{currentMoneyCoin}";
        
            if (currentMoneyFeatherText != null) 
                currentMoneyFeatherText.text = $"{currentMoneyFeather}";
        }
    }
}