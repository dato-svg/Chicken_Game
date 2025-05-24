using UnityEngine;

namespace BaseScripts.Spinner
{
    public class PrizeItem : MonoBehaviour
    {
        [SerializeField] private string prizeName;

        public void ShowResult()
        {
            Debug.Log("Ты выиграл: " + prizeName);
        }
    }
}