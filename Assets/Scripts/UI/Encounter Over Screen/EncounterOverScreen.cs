using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncounterOverScreen : MonoBehaviour
{
    // [SerializeField] private EncounterOverText encounterOverText;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button quitButton;


    public void Show(bool playerWon)
    {
        gameObject.SetActive(true);
        if (playerWon)
        {
            textMesh.text = "Victory";
            nextButton.onClick.AddListener(GameManagement.GameManager.LoadMainMenu);
        }
        else
        {
            textMesh.text = "Defeat";
            nextButton.onClick.AddListener(GameManagement.GameManager.LoadMainMenu);
        }
        quitButton.onClick.AddListener(GameManagement.GameManager.ExitGame);
    }
}
