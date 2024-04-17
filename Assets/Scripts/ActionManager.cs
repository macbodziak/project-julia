using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private static ActionManager _instance;
    Unit selectedUnit;


    public static ActionManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SetSelectedUnit(Unit newSelectedUnit)
    {
        if (selectedUnit != null)
        {
            selectedUnit.SetSelectionVisual(false);
        }

        if (newSelectedUnit == null)
        {
            return;
        }

        selectedUnit = newSelectedUnit;
        selectedUnit.SetSelectionVisual(true);

    }

    public void ClearUnitSelection()
    {
        selectedUnit = null;
    }


}
