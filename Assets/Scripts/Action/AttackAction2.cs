using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction2 : BaseAction
{
    public override String Name()
    {
        return "Attack2";
    }

    public override void StartAction()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Attack");
    }
}
