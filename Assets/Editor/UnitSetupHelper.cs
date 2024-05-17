using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitSetupHelper : EditorWindow
{
    Label infoLabel;
    Toggle isPlayerToggle;
    IntegerField numberOfActionBehaviours;
    string infoLabelText;
    [SerializeField]
    VisualTreeAsset uxmlTree;

    [MenuItem("Window/Project Julia/Unit Setup Helper")]
    public static void ShowExample()
    {
        UnitSetupHelper wnd = GetWindow<UnitSetupHelper>();
        wnd.titleContent = new GUIContent("Unit Setup Helper");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Unit Setup Helper");
        root.Add(label);

        TwoPaneSplitView paneSplitView = new TwoPaneSplitView(0, 350, TwoPaneSplitViewOrientation.Horizontal);
        infoLabel = new Label();
        VisualElement leftPane = uxmlTree.Instantiate();
        leftPane.style.minWidth = 220;
        paneSplitView.Add(leftPane);
        paneSplitView.Add(infoLabel);
        root.Add(paneSplitView);

        isPlayerToggle = root.Query<Toggle>("IsPlayerToggle");
        numberOfActionBehaviours = root.Query<IntegerField>("NumberOfActions");
        Button button = root.Query<Button>("SetupUnitButton");
        button.clicked += OnUnitCreationClicked;

    }


    private void OnUnitCreationClicked()
    {
        if (Selection.activeGameObject != null)
        {
            infoLabelText = "";
            CreateUnit(Selection.activeGameObject);
        }
        else
        {
            infoLabelText = "No Game Object selected";
        }
        infoLabel.text = infoLabelText;
        infoLabel.MarkDirtyRepaint();
    }

    private void CreateUnit(GameObject gameObject)
    {
        gameObject.layer = LayerMask.GetMask("Units");
        AddComponent<Unit>(gameObject);
        AddComponent<CombatStats>(gameObject);
        AddComponent<ActionController>(gameObject);
        AddComponent<StatusEffectController>(gameObject);
        AddComponent<Animator>(gameObject);

        //TO DO check how many already there are
        if (numberOfActionBehaviours.value > 0)
        {
            for (int i = 0; i < numberOfActionBehaviours.value; i++)
            {
                gameObject.AddComponent<ActionBehaviour>();
            }
            infoLabelText += "adding component ActionBehaviour x " + numberOfActionBehaviours.value + " \n";
        }

        AddComponent<CapsuleCollider>(gameObject);


        //set isPlayer flag of Unit componenet and create Behavior Tree for Enemies
        if (isPlayerToggle.value == false)
        {
            SerializedObject so = new SerializedObject(gameObject.GetComponent<Unit>());
            SerializedProperty isPlayerProperty = so.FindProperty("isPlayer");
            isPlayerProperty.boolValue = false;
            so.ApplyModifiedProperties();
            AddComponent<BehaviorDesigner.Runtime.BehaviorTree>(gameObject);
        }
        else
        {
            SerializedObject so = new SerializedObject(gameObject.GetComponent<Unit>());
            SerializedProperty isPlayerProperty = so.FindProperty("isPlayer");
            isPlayerProperty.boolValue = true;
            so.ApplyModifiedProperties();
        }

        gameObject.layer = LayerMask.GetMask("Units");
        // gameObject.layer = (1 << 3);

        infoLabelText += "Setting layer mask to \"Units\"";

        AddSelectedVisualObject(gameObject);

        infoLabelText += "\n\nTo Do";
        if (isPlayerToggle.value == false)
        {
            infoLabelText += "\nAdd animation controller\nconfigure Behavior Tree if enemy";
        }
    }

    private T AddComponent<T>(GameObject gameObject) where T : UnityEngine.Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null)
        {
            component = gameObject.AddComponent<T>();
            infoLabelText += "adding component " + typeof(T) + "\n";
        }

        return component;
    }

    private void AddSelectedVisualObject(GameObject gameObject)
    {
        SelectedVisual visualCompopnent = AddComponent<SelectedVisual>(gameObject) as SelectedVisual;

        GameObject svPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/SelectedVisual.prefab", typeof(GameObject)) as GameObject;
        if (svPrefab == null)
        {
            infoLabelText += "<color=#ff5e5e>Unable to load Selected Visual Prefab</color>";
        }
        else
        {
            infoLabelText += "Adding Selected Visual Object as Child";
            GameObject go = Instantiate<GameObject>(svPrefab, gameObject.transform);

            SerializedObject so = new SerializedObject(visualCompopnent);
            SerializedProperty isPlayerProperty = so.FindProperty("meshRenderer");
            isPlayerProperty.objectReferenceValue = go;
            so.ApplyModifiedProperties();
        }
    }
}
