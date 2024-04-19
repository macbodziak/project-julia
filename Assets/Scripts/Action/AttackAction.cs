using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BaseAction
{
    public override String Name()
    {
        return "Attack";
    }

    public override void StartAction()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Attack");
    }

    public override ActionType Type()
    {
        return ActionType.SingleEnemyTarget;
    }
}
