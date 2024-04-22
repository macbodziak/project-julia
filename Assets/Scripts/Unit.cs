using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Unit : MonoBehaviour
{
    SelectedVisual selectedVisual;
    [SerializeField] List<BaseAction> actionList;
    [SerializeField] bool isPlayer;

    [SerializeField] int maxHealthPoints;
    //Debug - Serialized for debugging only
    [SerializeField] int currentHealthPoints;
    public bool IsPlayer
    {
        get { return isPlayer; }
    }

    void Awake()
    {
        currentHealthPoints = maxHealthPoints;
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
        currentHealthPoints -= damage;

        if (currentHealthPoints <= 0)
        {
            currentHealthPoints = 0;
            Debug.Log(gameObject + "  DIED");
        }
    }
}
