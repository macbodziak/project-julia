using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInputState
{
    protected RaycastHit2D GetRaycastHit()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        return Physics2D.Raycast(mousePos2D, Vector2.zero);
    }

    public abstract void HandleInput();
    public virtual void OnEnter() {; }
    public virtual void OnExit() {; }

}
