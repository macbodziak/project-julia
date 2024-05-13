using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using UnityEngine;

public class SelectMultipleEnemyTargetsState : BaseInputState
{
    private int numberOfTargets;
    private Unit hoveredOverUnit;

    private List<Unit> selectedTargets;

    public SelectMultipleEnemyTargetsState()
    {
        selectedTargets = new List<Unit>();
    }

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
                    if (hitUnit != hoveredOverUnit)
                    {
                        if (hoveredOverUnit != null)
                        {
                            hoveredOverUnit.SetSelectionVisual(false);
                        }
                        hitUnit.SetSelectionVisual(true);
                        hoveredOverUnit = hitUnit;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (selectedTargets.Contains(hitUnit) == false)
                        {
                            selectedTargets.Add(hitUnit);
                            hitUnit.SetSelectionVisual(true);
                        }

                        if (selectedTargets.Count == numberOfTargets)
                        {
                            //finished selecting
                            ActionManager.Instance.SetTargetList(selectedTargets);
                            ActionManager.Instance.StartSelectedAction();
                        }
                    }
                }
            }
        }
        else
        {
            if (hoveredOverUnit != null)
            {
                if (selectedTargets.Contains(hoveredOverUnit) == false)
                {
                    hoveredOverUnit.SetSelectionVisual(false);
                }
            }
            hoveredOverUnit = null;
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (selectedTargets.Count == 0)
            {
                InputManager.Instance.SetState(InputState.SelectUnitAndAction);
            }
            else
            {
                foreach (Unit target in selectedTargets)
                {
                    target.SetSelectionVisual(false);
                }
                selectedTargets.Clear();
            }
        }
    }

    public override void OnEnter()
    {
        numberOfTargets = ActionManager.Instance.SelectedAction.numberOfTargets;
        if (numberOfTargets > CombatEncounterManager.Instance.GetEnemyCount())
        {
            numberOfTargets = CombatEncounterManager.Instance.GetEnemyCount();
        }
    }

    public override void OnExit()
    {
        foreach (Unit target in selectedTargets)
        {
            target.SetSelectionVisual(false);
        }
        if (hoveredOverUnit != null)
        {
            hoveredOverUnit.SetSelectionVisual(false);
        }
        selectedTargets.Clear();
    }

}
