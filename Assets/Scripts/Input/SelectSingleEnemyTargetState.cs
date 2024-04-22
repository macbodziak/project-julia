using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SelectSingleEnemyTargetState : BaseInputState
{
    private Unit hoveredOverUnit;
    public override void HandleInput()
    {
        RaycastHit2D hit = GetRaycastHit();
        if (hit.collider != null)
        {
            GameObject hitGameObject = hit.collider.transform.parent.gameObject;
            Unit hitUnit = hitGameObject.GetComponent<Unit>();
            if (hitUnit != null)
            {
                if (hitUnit.IsPlayer == false)
                {
                    if (hitUnit != hoveredOverUnit)
                    {
                        if (hoveredOverUnit != null)
                        {
                            hoveredOverUnit.SetSelectionVisual(false);
                        }
                        hitUnit.SetSelectionVisual(true);
                        hoveredOverUnit = hitUnit;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        ActionManager.Instance.SetSingleTarget(hitUnit);
                        ActionManager.Instance.StartSelectedAction();
                    }
                }
            }
        }
        else
        {
            if (hoveredOverUnit != null)
            {
                hoveredOverUnit.SetSelectionVisual(false);
            }
            hoveredOverUnit = null;
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InputManager.Instance.SetInputState(InputManager.State.SelectUnitAndAction);
        }
    }

    public override void OnExit()
    {
        if (hoveredOverUnit != null)
        {
            hoveredOverUnit.SetSelectionVisual(false);
        }
    }
}

