using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSingleAllyTargetState : BaseInputState
{
    private Unit hoveredOverUnit;
    public override void HandleInput()
    {
        GameObject hitGameObject = RaycastToGameObject();
        if (hitGameObject != null)
        {
            Unit hitUnit = hitGameObject.GetComponent<Unit>();
            if (hitUnit != null)
            {
                if (hitUnit.IsPlayer == true && hitUnit != ActionManager.Instance.SelectedUnit)
                {
                    if (hitUnit != hoveredOverUnit)
                    {
                        if (hoveredOverUnit != null)
                        {
                            hoveredOverUnit.SetSelectionVisual(true);
                        }
                        hitUnit.SetSelectionVisual(false);
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
            InputManager.Instance.SetState(InputState.SelectUnitAndAction);
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

