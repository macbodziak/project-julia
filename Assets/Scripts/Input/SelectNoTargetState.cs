using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectNoTargetState : BaseInputState
{
    public override void HandleInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            ActionManager.Instance.StartSelectedAction();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InputManager.Instance.CurrentState = InputState.SelectUnitAndAction;
        }
    }
}

