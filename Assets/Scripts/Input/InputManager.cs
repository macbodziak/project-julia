using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System;

public class InputManager : MonoBehaviour
{
    public enum State
    {
        SelectUnitAndAction,
        SelectSingleEnemyTarget,
        NULL
    };
    private static InputManager _instance;
    public event EventHandler OnInputStateChanged;
    private BaseInputState currentInputState;
    State currentState;
    State nextState;
    bool isStateChangeRequested;
    BaseInputState[] inputStateArray = {
        new SelectUnitAndActionState(),
        new SelectSingleEnemyTargetState()
        };

    public static InputManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            InitializationStateOnAwake();
        }
    }

    private void Update()
    {
        currentInputState.HandleInput();

        if (isStateChangeRequested)
        {
            TransitionInputState();
        }
    }


    //This funtion handles the transition, but setting the state to be transitioned is done via SetInputState(State state)
    private void TransitionInputState()
    {
        currentInputState.OnExit();
        currentState = nextState;
        currentInputState = inputStateArray[(int)nextState];
        Debug.Log("Changing Input state to" + nextState + "");
        nextState = State.NULL;
        currentInputState.OnEnter();
        isStateChangeRequested = false;
    }

    //Set the state to be transitioned to. The actual transition happen it TransitionInputState() at the end of Update(), after finishing handling input form previous state
    public void SetInputState(State state)
    {
        if (state == currentState)
        {
            return;
        }
        nextState = state;
        isStateChangeRequested = true;
    }

    private void InitializationStateOnAwake()
    {
        isStateChangeRequested = false;
        currentState = State.SelectUnitAndAction;
        nextState = State.NULL;
        currentInputState = inputStateArray[(int)currentState];
    }
}
