using UnityEngine;

namespace BaseScripts
{
    public class FollowWithOffset : MonoBehaviour
    {
        [SerializeField] private RectTransform target;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float followSpeed = 500f;

        private void Update()
        {
            if (target == null) return;
            
            transform.position = Vector3.Lerp(transform.position, target.position + offset, followSpeed * Time.deltaTime);
        }
    }
}
