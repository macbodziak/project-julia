using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectActionState : BaseInputState
{
    public override void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //This is the same code as in Unit Selection State, as you can still select another unit at this state
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
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
            //CHANGE TO SELECTUNITSTATE
            InputManager.Instance.SetInputState(InputManager.State.SelectUnitAndAction);
        }

        // if (Input.GetKeyDown(KeyCode.Alpha1))
        // {

        // }
    }
}
