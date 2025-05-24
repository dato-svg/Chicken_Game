using System;
using UnityEngine;
using UnityEngine.UI;

namespace BaseScripts
{
    public class ExitButtonController : MonoBehaviour
    {
        private static readonly int Exit = Animator.StringToHash("Exit");
        private static readonly int Enter = Animator.StringToHash("Enter");


        [SerializeField] private GameObject deactivatePanel;
        [SerializeField] private bool activator;
        
        private Button _button;
        
        private Animator _animator;


        private void Start() => 
            _animator = deactivatePanel.GetComponent<Animator>();

        private void OnEnable()
        {
            _button = GetComponent<Button>();
            if (activator == false)
            {
                _button.onClick.AddListener(DisablePanel);
            }
            else
            {
                _button.onClick.AddListener(EnablePanel);
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(DisablePanel);
            _button.onClick.RemoveListener(EnablePanel);
        }

        private void EnablePanel()
        {
            _animator.SetTrigger(Enter);
            deactivatePanel.SetActive(true);
        }

        private void DisablePanel()
        {
           // deactivatePanel.SetActive(false);
            _animator.SetTrigger(Exit);
        }

    }
}
