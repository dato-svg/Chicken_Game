using System.Collections;
using UnityEngine;

namespace BaseScripts
{
    public class FeatherSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject featherPrefab;
        [SerializeField] private float spawnInterval = 30f;
        [SerializeField] private float minX = -500f;
        [SerializeField] private float maxX = 500f;
        [SerializeField] private float spawnY = 800f;
        [SerializeField] private RectTransform spawnParent;    
        
        private Coroutine _spawnCoroutine;
        
        private void Start() => 
            StartSpawn();
        
        private void StartSpawn()
        {
            StopSpawn();
            _spawnCoroutine = StartCoroutine(SpawnFeathersRoutine());
        }
        
        private void StopSpawn()
        {
            if (_spawnCoroutine == null) return;
            
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
        
        private IEnumerator SpawnFeathersRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnInterval);
                SpawnFeather();
            }
        }
        
        private void SpawnFeather()
        {
            if (featherPrefab == null || spawnParent == null)
                return;
            
            float randomX = Random.Range(minX, maxX);
            
            Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);
            
            float randomRotationZ = Random.Range(-180f, 180f);
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomRotationZ);
            
            GameObject feather = Instantiate(featherPrefab, spawnPosition,randomRotation,spawnParent);
            feather.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
            feather.transform.SetAsLastSibling();
        }
    }
}
