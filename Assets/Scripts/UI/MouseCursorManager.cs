using System;
using TMPro;
using UnityEngine;

public class MouseCursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D attackCursor;
    [SerializeField] private TextMeshProUGUI textPrefab;
    [SerializeField] public Vector3 cursorTextOffset = new Vector3(22, 22, 0);
    private TextMeshProUGUI cursorText;

    public Texture2D AttackCursor { get => attackCursor; private set => attackCursor = value; }

    void Start()
    {
        cursorText = Instantiate<TextMeshProUGUI>(textPrefab, transform);
        SetState(MouseCursorStateType.Default);
        cursorText.text = "";

        Unit.OnMouseEnterAnyUnit += HandleMouseEnterAnyUnit;
        Unit.OnMouseExitAnyUnit += HandleMouseExitAnyUnit;
    }

    private void HandleMouseExitAnyUnit(object sender, EventArgs e)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        cursorText.text = "";
        SetState(MouseCursorStateType.Default);
    }

    private void HandleMouseEnterAnyUnit(object sender, EventArgs e)
    {
        Unit unit = (Unit)sender;
        if (unit.IsPlayer == false &&
            (InputManager.Instance.CurrentState == InputState.SelectSingleEnemyTarget ||
            InputManager.Instance.CurrentState == InputState.SelectMultipleEnemyTargets ||
            InputManager.Instance.CurrentState == InputState.SelectAllEnemyTargets))
        {
            string newCursorText;
            TryHitChanceToText(out newCursorText, unit);
            cursorText.text = newCursorText;
            SetState(MouseCursorStateType.Attack);
        }
    }

    private void Update()
    {
        cursorText.transform.position = Input.mousePosition + cursorTextOffset;
    }

    private void OnDestroy()
    {
        Unit.OnMouseEnterAnyUnit -= HandleMouseEnterAnyUnit;
        Unit.OnMouseExitAnyUnit -= HandleMouseExitAnyUnit;
    }

    public void SetState(MouseCursorStateType nextState)
    {
        switch (nextState)
        {
            case MouseCursorStateType.Default:
                cursorText.enabled = false;
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                break;
            case MouseCursorStateType.Attack:
                cursorText.enabled = true;
                Cursor.SetCursor(AttackCursor, Vector2.zero, CursorMode.Auto);
                break;
            default:
                cursorText.enabled = false;
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                break;
        }
    }

    private bool TryHitChanceToText(out string hitChancetext, Unit target)
    {
        ICanAttack attackDefinition = ActionManager.Instance.SelectedAction.actionDefinition as ICanAttack;
        if (attackDefinition != null)
        {
            AttackInfo attackInfo = attackDefinition.GetAttackInfo(ActionManager.Instance.SelectedUnit.combatStats);
            hitChancetext = (100 - target.combatStats.GetRequiredHitRoll(attackInfo)) + "%";
            return true;
        }
        hitChancetext = "";
        return false;
    }
}
