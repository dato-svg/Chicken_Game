using System.Collections;
using TMPro;
using UnityEngine;

namespace BaseScripts
{
    public class FloatingTextSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] floatingPrefabs;
        [SerializeField] private Transform parentUI;

        [SerializeField] private float moveUpDistance = 100f;
        [SerializeField] private float duration = 1.2f;
        
       
        public void SpawnAndAnimate(int prefabIndex, string text)
        {
            if (prefabIndex < 0 || prefabIndex >= floatingPrefabs.Length || floatingPrefabs[prefabIndex] == null)
            {
                Debug.LogWarning("Неверный индекс префаба или не назначен префаб!");
                return;
            }

            GameObject instance = Instantiate(floatingPrefabs[prefabIndex], parentUI);
            instance.GetComponent<TextMeshProUGUI>().text = text.ToString();
            RectTransform rect = instance.GetComponent<RectTransform>();
            if (rect == null)
            {
                Debug.LogError("Prefab должен иметь RectTransform!");
                Destroy(instance);
                return;
            }

            rect.anchoredPosition = Vector2.zero;

            CanvasGroup canvasGroup = instance.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = instance.AddComponent<CanvasGroup>();

            StartCoroutine(AnimateAndDestroy(instance, rect, canvasGroup));
        }

        private IEnumerator AnimateAndDestroy(GameObject obj, RectTransform rect, CanvasGroup canvasGroup)
        {
            float elapsed = 0f;
            Vector2 startPos = rect.anchoredPosition;
            Vector2 endPos = startPos + new Vector2(0, moveUpDistance);

            while (elapsed < duration)
            {
                float t = elapsed / duration;

                rect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);

                elapsed += Time.deltaTime;
                yield return null;
            }

            Destroy(obj);
        }
    }
}
