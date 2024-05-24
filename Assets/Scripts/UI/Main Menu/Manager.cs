using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI loadingText;




        private void Start()
        {
            Debug.Assert(loadingText);
        }



        public void NewGame()
        {
            loadingText.enabled = true;
            GameManagement.GameManager.NewGame();
        }
    }
}