using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitOverviewPanel : MonoBehaviour
{
    [SerializeField] private PortraitBehavior portraitPrefab;
    [SerializeField] private GameObject playerUnitsPanel;
    [SerializeField] private GameObject enemyUnitsPanel;
    [SerializeField] private Sprite defaultSprite;


    public void Setup(List<Unit> playerUnits, List<Unit> enemyUnits)
    {

        foreach (Unit unit in playerUnits)
        {
            PortraitBehavior portrait = Instantiate<PortraitBehavior>(portraitPrefab);
            portrait.Setup(unit);
            portrait.transform.SetParent(playerUnitsPanel.transform);
        }

        foreach (Unit unit in enemyUnits)
        {
            PortraitBehavior portrait = Instantiate<PortraitBehavior>(portraitPrefab);
            portrait.Setup(unit);
            portrait.transform.SetParent(enemyUnitsPanel.transform);
        }
    }
}
