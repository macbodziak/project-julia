using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitAndActionState : BaseInputState
{
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
        //  check if selected Unit still is alive and active
        // ActionManager.Instance.SelectedUnit = null;
        ActionManager.Instance.SelectedAction = null;
        ActionManager.Instance.ClearTargetList();
    }
}
