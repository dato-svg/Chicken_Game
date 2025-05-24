using UnityEngine;

namespace BaseScripts.Test
{
    public class TestDrag : MonoBehaviour
    {
        public Vector3 _offset;
        public Camera _camera;

        private Vector3 _originalPosition;

        private void Start()
        {
            if (_camera == null)
                _camera = Camera.main;
        }

        private void OnMouseDown()
        {
            _originalPosition = transform.position;

            Vector3 mouseWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = transform.position.z;
            _offset = transform.position - mouseWorld;
        }

        private void OnMouseDrag()
        {
            Vector3 mouseWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = transform.position.z;
            transform.position = mouseWorld + _offset;
        }

        private void OnMouseUp()
        {
            transform.position = _originalPosition;
        }
    }
}