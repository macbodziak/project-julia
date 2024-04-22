using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitAndActionState : BaseInputState
{
    public override void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = GetRaycastHit();
            if (hit.collider != null)
            {
                GameObject clickedGameObject = hit.collider.transform.parent.gameObject;
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
        ActionManager.Instance.SelectedUnit = null;
        ActionManager.Instance.ClearTargetList();
    }
}
