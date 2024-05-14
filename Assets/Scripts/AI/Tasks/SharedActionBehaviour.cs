using BehaviorDesigner.Runtime;

[System.Serializable]
public class SharedActionBehaviour : SharedVariable<ActionBehaviour>
{
    public static implicit operator SharedActionBehaviour(ActionBehaviour value) { return new SharedActionBehaviour { Value = value }; }
}