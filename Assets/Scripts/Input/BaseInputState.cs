using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInputState
{
    public abstract void HandleInput();
    public virtual void OnEnter() {; }
    public virtual void OnExit() {; }

}
