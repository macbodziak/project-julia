using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterOverScreenState : BaseInputState
{
    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Leave this screen?");
        }
    }
}
