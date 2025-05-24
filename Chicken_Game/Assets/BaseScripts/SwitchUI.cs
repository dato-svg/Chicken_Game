using UnityEngine;

namespace BaseScripts
{
    public class SwitchUI : MonoBehaviour
    {
        private static readonly int Enter = Animator.StringToHash("Enter");
        
        [SerializeField] private GameObject eggMenu;
        [SerializeField] private GameObject chickenMenu;


        public void ActiveEggMenu()
        {
            chickenMenu.SetActive(false);
            eggMenu.SetActive(true);
            eggMenu.GetComponent<Animator>().SetTrigger(Enter);
        }

        public void ActiveChickenMenu()
        {
            eggMenu.SetActive(false);
            chickenMenu.SetActive(true);
            chickenMenu.GetComponent<Animator>().SetTrigger(Enter);
        }
    }
}
