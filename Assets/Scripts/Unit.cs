using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Animator anim;
    SelectedVisual selectedVisual;
    [SerializeField] List<BaseAction> actionList;
    [SerializeField] bool isPlayer;

    public bool IsPlayer
    {
        get { return isPlayer; }
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        selectedVisual = GetComponent<SelectedVisual>();
        GetComponents<BaseAction>(actionList);
    }



    public void Attack()
    {
        // anim.SetTrigger("Attack");
    }

    public void Hurt()
    {
        anim.SetTrigger("Hurt");
    }

    public void SetSelectionVisual(bool isVisible)
    {
        selectedVisual.SetSelectedVisual(isVisible);
    }

    public List<BaseAction> GetActionList()
    {
        return actionList;
    }
}
