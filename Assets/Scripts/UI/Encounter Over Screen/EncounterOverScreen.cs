using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterOverScreen : MonoBehaviour
{
    [SerializeField] private EncounterOverText encounterOverText;


    public void Show(bool playerWon)
    {
        encounterOverText.Show(playerWon);
    }
}
