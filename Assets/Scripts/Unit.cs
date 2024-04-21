using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    SelectedVisual selectedVisual;
    [SerializeField] List<BaseAction> actionList;
    [SerializeField] bool isPlayer;

    [SerializeField]
    int healthPoints;
    public bool IsPlayer
    {
        get { return isPlayer; }
    }

    void Awake()
    {
        selectedVisual = GetComponent<SelectedVisual>();
        GetComponents<BaseAction>(actionList);
    }


    public void SetSelectionVisual(bool isVisible)
    {
        selectedVisual.SetSelectedVisual(isVisible);
    }

    public List<BaseAction> GetActionList()
    {
        return actionList;
    }

    public void TakeDamage(int damage)
    {

    }
}
