using System.Collections;
using System.Collections.Generic;
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

}
