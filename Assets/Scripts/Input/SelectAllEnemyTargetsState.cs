using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAllEnemyTargetsState : BaseInputState
{
    private bool isHovereingOverAnyUnit;
    public override void HandleInput()
    {
        GameObject hitGameObject = RaycastToGameObject();
        if (hitGameObject != null)
        {
            Unit hitUnit = hitGameObject.GetComponent<Unit>();
            if (hitUnit != null)
            {
                if (hitUnit.IsPlayer == false)
                {
                    if (isHovereingOverAnyUnit == false)
                    {
                        Debug.Log("enabling all visuals");
                        foreach (Unit enemyUnit in CombatEncounterManager.Instance.GetEnemyUnitList())
                        {
                            enemyUnit.SetSelectionVisual(true);
                        }
                        isHovereingOverAnyUnit = true;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        ActionManager.Instance.SetTargetList(CombatEncounterManager.Instance.GetEnemyUnitList());
                        ActionManager.Instance.StartSelectedAction();
                    }
                }
            }
        }
        else
        {
            if (isHovereingOverAnyUnit == true)
            {
                Debug.Log("clearing all visuals");
                foreach (Unit enemyUnit in CombatEncounterManager.Instance.GetEnemyUnitList())
                {
                    enemyUnit.SetSelectionVisual(false);
                }
                isHovereingOverAnyUnit = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (Unit enemyUnit in CombatEncounterManager.Instance.GetEnemyUnitList())
            {
                enemyUnit.SetSelectionVisual(false);
            }
            InputManager.Instance.CurrentState = InputManager.State.SelectUnitAndAction;
        }
    }

    public override void OnExit()
    {
        if (isHovereingOverAnyUnit == true)
        {
            foreach (Unit enemyUnit in CombatEncounterManager.Instance.GetEnemyUnitList())
            {
                enemyUnit.SetSelectionVisual(false);
            }
            isHovereingOverAnyUnit = false;
        }
    }
}

