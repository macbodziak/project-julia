using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealingReceivedEventArgs : EventArgs
{
    public int Amount { get; private set; }

    public HealingReceivedEventArgs(int amount)
    {
        Amount = amount;
    }

}
