using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SelectSingleEnemyTargetState : BaseInputState
{
    private Unit hoveredOverUnit;
    public override void HandleInput()
    {
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
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
                            Debug.Log(hitUnit + " choosen as target");
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

