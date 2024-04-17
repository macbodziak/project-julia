using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Animator anim;
    SelectedVisual selectedVisual;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        selectedVisual = GetComponent<SelectedVisual>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Hurt();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            selectedVisual.Toggle();
        }
    }


    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    void Hurt()
    {
        anim.SetTrigger("Hurt");
    }

    public void SetSelectionVisual(bool isVisible)
    {
        selectedVisual.SetSelectedVisual(isVisible);
    }
}
