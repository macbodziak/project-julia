using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterOverEventArgs : EventArgs
{
    public bool PlayerWon { get; private set; }

    public EncounterOverEventArgs(bool playerWon)
    {
        PlayerWon = playerWon;
    }
}
