using TMPro;
using UnityEngine;

namespace BaseScripts
{
    public class GridPriceUI : MonoBehaviour
    {
          [SerializeField] private TextMeshProUGUI featherPriceText;
          [SerializeField] private PlaceLockController placeLock;

          private void Awake()
          {
              featherPriceText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
              placeLock = GetComponentInParent<PlaceLockController>();
          }
          
          private void Start() => 
              featherPriceText.text = placeLock.featherPrice.ToString();
    }
}
