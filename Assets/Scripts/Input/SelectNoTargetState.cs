using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectNoTargetState : BaseInputState
{
    public override void HandleInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
            {
                ActionManager.Instance.StartSelectedAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InputManager.Instance.SetState(InputState.SelectUnitAndAction);
        }

        ProcessKeyboardActionSelection();
    }
}

