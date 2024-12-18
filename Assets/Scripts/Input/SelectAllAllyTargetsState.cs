using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAllAllyTargetsState : BaseInputState
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
                if (hitUnit.IsPlayer == true)
                {
                    if (isHovereingOverAnyUnit == false)
                    {
                        foreach (Unit playerUnit in GameManagement.EncounterManager.Instance.GetPlayerUnitList())
                        {
                            playerUnit.SetSelectionVisual(true);
                        }
                        isHovereingOverAnyUnit = true;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        ActionManager.Instance.SetTargetList(GameManagement.EncounterManager.Instance.GetPlayerUnitList());
                        ActionManager.Instance.StartSelectedAction();
                    }
                }
            }
        }
        else
        {
            if (isHovereingOverAnyUnit == true)
            {
                foreach (Unit playerUnit in GameManagement.EncounterManager.Instance.GetPlayerUnitList())
                {
                    if (playerUnit != ActionManager.Instance.SelectedUnit)
                    {
                        playerUnit.SetSelectionVisual(false);
                    }
                }
                isHovereingOverAnyUnit = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (Unit playerUnit in GameManagement.EncounterManager.Instance.GetPlayerUnitList())
            {
                if (playerUnit != ActionManager.Instance.SelectedUnit)
                {
                    playerUnit.SetSelectionVisual(false);
                }
            }
            InputManager.Instance.SetState(InputState.SelectUnitAndAction);
        }

        ProcessKeyboardActionSelection();
    }

    public override void OnExit()
    {
        if (isHovereingOverAnyUnit == true)
        {
            foreach (Unit playerUnit in GameManagement.EncounterManager.Instance.GetPlayerUnitList())
            {
                if (playerUnit != ActionManager.Instance.SelectedUnit)
                {
                    playerUnit.SetSelectionVisual(false);
                }
            }
            isHovereingOverAnyUnit = false;
        }
    }
}

