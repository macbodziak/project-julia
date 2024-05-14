using UnityEngine;

public class SelectSelfTargetState : BaseInputState
{
    public override void HandleInput()
    {
        GameObject hitGameObject = RaycastToGameObject();
        if (hitGameObject != null)
        {
            Unit hitUnit = hitGameObject.GetComponent<Unit>();
            if (hitUnit != null)
            {
                if (hitUnit == ActionManager.Instance.SelectedUnit)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        ActionManager.Instance.SetSingleTarget(hitUnit);
                        ActionManager.Instance.StartSelectedAction();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InputManager.Instance.SetState(InputState.SelectUnitAndAction);
        }
    }
}

