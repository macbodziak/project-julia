using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public event EventHandler InputStateChangedEvent;
    private BaseInputState currentInputStateHandler;
    private InputState currentState;
    private InputState nextState;
    private bool isStateChangeRequested;

    public InputState CurrentState
    {
        get { return currentState; }
    }

    // this array of input states must be the same as the order of enums in InputState
    // there has to be an input state class created for derived from BaseInputState 
    private BaseInputState[] inputStateArray = {
        new SelectUnitAndActionState(),
        new SelectSingleEnemyTargetState(),
        new SelectMultipleEnemyTargetsState(),
        new SelectAllEnemyTargetsState(),
        new SelectSelfTargetState(),
        new SelectSingleAllyTargetState(),
        new SelectAllAllyTargetsState(),
        new SelectNoTargetState(),
        new EncounterOverScreenState(),
        new InputBlockedState(),
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
    // via SetState(State state).
    // should not be called form outside of this class
    private void TransitionInputState()
    {
        currentInputStateHandler.OnExit();
        currentState = nextState;
        currentInputStateHandler = inputStateArray[(int)nextState];
        nextState = InputState.NULL;
        currentInputStateHandler.OnEnter();
        isStateChangeRequested = false;
        InputStateChangedEvent?.Invoke(this, EventArgs.Empty);
    }

    //
    // Summary:
    // Set the state to be transitioned to. The actual transition happen it TransitionInputState()
    // at the end of Update(), after finishing handling input form previous state
    // This method should be called from outside this class to request a state change
    public void SetState(InputState state)
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
        //state related variables
        isStateChangeRequested = false;
        currentState = InputState.SelectUnitAndAction;
        nextState = InputState.NULL;
        currentInputStateHandler = inputStateArray[(int)currentState];
    }
}
