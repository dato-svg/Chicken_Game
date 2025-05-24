using UnityEngine;

namespace BaseScripts.Configs
{
    [CreateAssetMenu(fileName = "newChickenConfig", menuName = "ChickenConfig", order = 1)]
    public class ChickenConfig : ScriptableObject
    {
        [Header("Доход")]
        [SerializeField] private int passiveIncome = 2;
        [SerializeField] private int clickIncome = 5;

        public int PassiveIncome {get => passiveIncome;set => passiveIncome = value; }
        public int ClickIncome { get => clickIncome; set => clickIncome = value; }
    }
}