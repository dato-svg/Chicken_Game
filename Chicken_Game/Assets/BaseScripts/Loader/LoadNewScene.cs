using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaseScripts.Loader
{
    public class LoadNewScene : MonoBehaviour
    {
        [SerializeField] private GameObject[] activateObject;
        
        private Coroutine _coroutine;
        
        
        
        private void Start()
        {
            DeactivateObject();
            StartLoading();
        }
        

        private IEnumerator StartActivate()
        {
            foreach (var egg in activateObject)
            {
                egg.SetActive(true);
                yield return new WaitForSeconds(0.4f);
            }
            
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        

        private void StartLoading()
        {
            StopLoading();
            _coroutine = StartCoroutine(StartActivate());
        }

        private void StopLoading()
        {
            if (_coroutine == null)
                return;
            
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private void DeactivateObject()
        {
            foreach (var egg in activateObject)
            {
                egg.SetActive(false);
            }
        }
    }
}
