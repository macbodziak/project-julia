using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectUnitAndActionState : BaseInputState
{
    private Unit lastSelectedUnit;

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
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActionManager.Instance.SelectedUnit = null;
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
