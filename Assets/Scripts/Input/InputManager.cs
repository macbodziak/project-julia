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
        Blocked,
        NULL
    };
    private static InputManager _instance;
    public event EventHandler OnInputStateChanged;
    private BaseInputState currentInputStateHandler;
    State currentState;
    State nextState;
    bool isStateChangeRequested;
    bool isActionInProgress;
    BaseInputState[] inputStateArray = {
        new SelectUnitAndActionState(),
        new SelectSingleEnemyTargetState(),
        new InputBlockedState()
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
        currentInputStateHandler.HandleInput();

        if (isStateChangeRequested)
        {
            TransitionInputState();
        }
    }

    //
    // Summary:
    // This funtion handles the transition, but setting the state to be transitioned is done 
    // via SetInputState(State state).
    // should not be called form outside of this class
    private void TransitionInputState()
    {
        currentInputStateHandler.OnExit();
        currentState = nextState;
        currentInputStateHandler = inputStateArray[(int)nextState];
        Debug.Log("Changing Input state to" + nextState + "");
        nextState = State.NULL;
        currentInputStateHandler.OnEnter();
        isStateChangeRequested = false;
        OnInputStateChanged?.Invoke(this, EventArgs.Empty);
    }

    //
    // Summary:
    // Set the state to be transitioned to. The actual transition happen it TransitionInputState()
    // at the end of Update(), after finishing handling input form previous state
    // This method should be called from outside this class to request a state change
    public void SetInputState(State state)
    {
        if (state == currentState)
        {
            return;
        }
        nextState = state;
        isStateChangeRequested = true;
    }

    public State GetInputState()
    {
        return currentState;
    }

    private void InitializationStateOnAwake()
    {
        isActionInProgress = false;

        //state related variables
        isStateChangeRequested = false;
        currentState = State.SelectUnitAndAction;
        nextState = State.NULL;
        currentInputStateHandler = inputStateArray[(int)currentState];
    }
}
