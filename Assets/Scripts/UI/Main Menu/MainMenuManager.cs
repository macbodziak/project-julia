using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;




    private void Start()
    {
        Debug.Assert(loadingText);
    }



    public void NewGame()
    {
        loadingText.enabled = true;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
}
