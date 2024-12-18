using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SelectSingleEnemyTargetState : BaseInputState
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
                if (hitUnit.IsPlayer == false)
                {
                    if (hitUnit != hoveredOverUnit)
                    {
                        if (hoveredOverUnit != null)
                        {
                            hoveredOverUnit.SetSelectionVisual(false);
                        }
                        hitUnit.SetSelectionVisual(true);
                        // Cursor.SetCursor(UIManager.Instance.AttackCursor, Vector2.zero, CursorMode.Auto);
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
                // Cursor.SetCursor(UIManager.Instance.DefaultCursor, Vector2.zero, CursorMode.Auto);
            }
            hoveredOverUnit = null;
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InputManager.Instance.SetState(InputState.SelectUnitAndAction);
        }

        ProcessKeyboardActionSelection();

    }

    public override void OnExit()
    {
        if (hoveredOverUnit != null)
        {
            hoveredOverUnit.SetSelectionVisual(false);
        }
    }
}

