using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectUnitAndActionState : BaseInputState
{
    private List<ActionBehaviour> selectedUnitsActions;

    public override void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clickedGameObject = RaycastToGameObject();
            if (clickedGameObject != null)
            {
                // GameObject clickedGameObject = hit.collider.transform.parent.gameObject;
                Unit clickedUnit = clickedGameObject.GetComponent<Unit>();
                if (clickedUnit != null)
                {
                    if (clickedUnit.IsPlayer)
                    {
                        ActionManager.Instance.SelectedUnit = clickedUnit;
                        selectedUnitsActions = clickedUnit.GetActionList();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selectedUnitsActions = null;
            ActionManager.Instance.SelectedUnit = null;
        }

        if (ActionManager.Instance.SelectedUnit != null)
        {
            ProcessKeyboardActionSelection();
        }
    }

    public override void OnEnter()
    {
        ActionManager.Instance.SelectedAction = null;
        // ActionManager.Instance.SelectedUnit = lastSelectedUnit;
        ActionManager.Instance.ClearTargetList();
    }


    public override void OnExit()
    {
        // lastSelectedUnit = ActionManager.Instance.SelectedUnit;
    }
}
