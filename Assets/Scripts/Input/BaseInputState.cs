using UnityEngine;

public abstract class BaseInputState
{
    const int unitLayerMask = 1 << 3;


    protected GameObject RaycastToGameObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hit, Mathf.Infinity, unitLayerMask);

        if (hit.collider == null)
        {
            return null;
        }
        return hit.collider.gameObject;
    }

    public abstract void HandleInput();


    public virtual void OnEnter() {; }


    public virtual void OnExit() {; }


    protected static void ProcessKeyboardActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSelectedAction(1);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetSelectedAction(2);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetSelectedAction(3);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetSelectedAction(4);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetSelectedAction(5);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetSelectedAction(6);
            return;
        }
    }


    private static void SetSelectedAction(int i)
    {
        if (ActionManager.Instance.SelectedUnit.GetActionList().Count >= i)
        {
            ActionManager.Instance.SelectedAction = ActionManager.Instance.SelectedUnit.GetActionList()[i - 1];
        }
    }
}
