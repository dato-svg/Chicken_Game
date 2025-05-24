using System.Collections;
using UnityEngine;

namespace BaseScripts.Spinner
{
    public class SpinnerMover : MonoBehaviour
    { 
        [Header("Spin Settings")]
        [SerializeField] private float minSpinTime = 2f; 
        [SerializeField] private float maxSpinTime = 4f;
        [SerializeField] private float maxSpeed = 720f; 
        [SerializeField] private AnimationCurve decelerationCurve;

        [Header("Arrow Settings")]
        [SerializeField] private Transform arrowTransform;
        [SerializeField] private float rayLength = 10f;
        [SerializeField] private LayerMask hitMask;

        private bool _isSpinning;

        [ContextMenu("StartSpin")]
        public void StartSpin()
        {
            if (!_isSpinning)
                StartCoroutine(SpinRoutine());
        }

        private IEnumerator SpinRoutine()
        {
            _isSpinning = true;

            float spinDuration = Random.Range(minSpinTime, maxSpinTime);
            float elapsedTime = 0f;

            while (elapsedTime < spinDuration)
            {
                transform.Rotate(Vector3.forward, maxSpeed * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            float slowdownTime = 0f;
            float slowdownDuration = 2f;

            while (slowdownTime < slowdownDuration)
            {
                float t = slowdownTime / slowdownDuration;
                float speed = maxSpeed * decelerationCurve.Evaluate(1f - t);
                transform.Rotate(Vector3.forward, speed * Time.deltaTime);
                slowdownTime += Time.deltaTime;
                yield return null;
            }

            _isSpinning = false;
            
            CheckHit();
        }

        private void CheckHit()
        {
            if (arrowTransform == null)
            {
                Debug.LogWarning("Arrow Transform не назначен!");
                return;
            }

            Vector3 direction = arrowTransform.up;
            Ray ray = new Ray(arrowTransform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, hitMask))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);
                Debug.Log("Попали в объект: " + hit.collider.name);
                
                PrizeItem prize = hit.collider.GetComponent<PrizeItem>();
                if (prize != null)
                {
                    prize.ShowResult();
                }
            }
            else
            {
                Debug.Log("Ничего не найдено по лучу.");
            }
        }
}
}

