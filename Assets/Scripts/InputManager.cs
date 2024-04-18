using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance { get { return _instance; } }
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                GameObject clickedGameObject = hit.collider.transform.parent.gameObject;
                Debug.Log(clickedGameObject.name + " clicked");

                Unit clickedUnit = clickedGameObject.GetComponent<Unit>();
                ActionManager.Instance.SetSelectedUnit(clickedUnit);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActionManager.Instance.SetSelectedUnit(null);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActionManager.Instance.TestingSetFirstAction();
        }
    }
}
