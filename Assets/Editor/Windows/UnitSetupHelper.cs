using System;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityLayerMask;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;


// <summary>
//The UnitSetupHelper class extends the Unity EditorWindow to create a custom editor window that assists 
// in setting up GameObjects as "units" with specific components and properties. 
// It provides a user interface with various controls and executes setup operations when a button is clicked.
// The CreateUnit method configures the selected GameObject by:
//     Setting its layer to "Units."
//     Adding required components such as Unit, CombatStats, ActionController, StatusEffectController, Animator, and CapsuleCollider.
//     Adding multiple ActionBehaviour components if specified.
//     Configuring the isPlayer property of the Unit component based on the toggle state.
//     Adding a BehaviorTree component for non-player units.
//     Adding a SelectedVisual component and its associated prefab as a child.
// </summary>
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

        TwoPaneSplitView paneSplitView = new TwoPaneSplitView(0, 350, TwoPaneSplitViewOrientation.Horizontal);
        infoLabel = new Label();
        VisualElement leftPane = uxmlTree.Instantiate();
        leftPane.style.minWidth = 220;
        paneSplitView.Add(leftPane);
        paneSplitView.Add(infoLabel);
        root.Add(paneSplitView);

        isPlayerToggle = root.Query<Toggle>("IsPlayerToggle");
        numberOfActionBehaviours = root.Query<IntegerField>("NumberOfActions");
        Button setupUnitButton = root.Query<Button>("SetupUnitButton");
        setupUnitButton.clicked += OnUnitCreationClicked;

        Button combatEncounterEnemyButton = root.Query<Button>("AddToCombatEncounterEnemy");
        combatEncounterEnemyButton.clicked += OnRegisterEnemyClicked;

        Button combatEncounterPlayerButton = root.Query<Button>("AddToCombatEncounterPlayer");
        combatEncounterPlayerButton.clicked += OnRegisterPlayerClicked;
    }


    private void OnRegisterPlayerClicked()
    {
        AddToCombatEncounter(Selection.activeGameObject, true);
        infoLabel.text = infoLabelText;
        infoLabel.MarkDirtyRepaint();
    }


    private void OnRegisterEnemyClicked()
    {
        AddToCombatEncounter(Selection.activeGameObject, false);
        infoLabel.text = infoLabelText;
        infoLabel.MarkDirtyRepaint();
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
        SetLayer(gameObject);
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
        AddComponent<SoundController>(gameObject);

        AudioSource audioSource = AddComponent<AudioSource>(gameObject);
        audioSource.playOnAwake = false;
        AudioMixer mixer = AssetDatabase.LoadAssetAtPath("Assets/Audio/AudioMixer.mixer", typeof(AudioMixer)) as AudioMixer;
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];


        //set isPlayer flag of Unit componenet and create Behavior Tree for Enemies
        if (isPlayerToggle.value == false)
        {
            SerializedObject so = new SerializedObject(gameObject.GetComponent<Unit>());
            SerializedProperty isPlayerProperty = so.FindProperty("isPlayer");
            isPlayerProperty.boolValue = false;
            so.ApplyModifiedProperties();
            BehaviorDesigner.Runtime.BehaviorTree BT = AddComponent<BehaviorDesigner.Runtime.BehaviorTree>(gameObject);
            BT.StartWhenEnabled = false;
        }
        else
        {
            SerializedObject so = new SerializedObject(gameObject.GetComponent<Unit>());
            SerializedProperty isPlayerProperty = so.FindProperty("isPlayer");
            isPlayerProperty.boolValue = true;
            so.ApplyModifiedProperties();
        }


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
        SelectedVisual visualCompopnent = gameObject.GetComponent<SelectedVisual>();
        if (visualCompopnent != null)
        {
            infoLabelText += "<color=#ff5e5e>Selected Visual component already attached</color>";
            return;
        }
        visualCompopnent = AddComponent<SelectedVisual>(gameObject) as SelectedVisual;

        GameObject svPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/SelectedVisual.prefab", typeof(GameObject)) as GameObject;
        if (svPrefab == null)
        {
            infoLabelText += "<color=#ff5e5e>Unable to load Selected Visual Prefab</color>";
        }
        else
        {
            infoLabelText += "Adding Selected Visual Object as Child\n";
            GameObject go = Instantiate<GameObject>(svPrefab, gameObject.transform);

            SerializedObject so = new SerializedObject(visualCompopnent);
            SerializedProperty isPlayerProperty = so.FindProperty("meshRenderer");
            isPlayerProperty.objectReferenceValue = go;
            so.ApplyModifiedProperties();
        }
    }


    private void SetLayer(GameObject gameObject)
    {

        SerializedObject so = new SerializedObject(gameObject);
        SerializedProperty layerProp = so.FindProperty("m_Layer");
        layerProp.intValue = LayerMask.NameToLayer("Units"); ;
        so.ApplyModifiedProperties();
        EditorUtility.SetDirty(gameObject);
        infoLabelText += "Setting layer mask to \"Units\"";
    }


    //<summary>
    //Call this method with a Unit GameObject and a boolean indicating if it's a player unit to register it with the combat encounter.
    ///<summary>
    private void AddToCombatEncounter(GameObject gameObject, bool isPlayer)
    {
        infoLabelText = "";
        if (gameObject == null)
        {
            infoLabelText += "<color=#ff5e5e>No Game Object selected</color>";
            return;

        }

        Unit unit = gameObject.GetComponent<Unit>();
        if (unit == null)
        {
            infoLabelText += "<color=#ff5e5e>No Unit selected</color>";
            return;
            // infoLabelText += "<color=#ff5e5e>U</color>";
        }

        SerializedObject combatEncounterManager = new SerializedObject(FindObjectOfType<CombatEncounterManager>());
        if (combatEncounterManager == null)
        {
            infoLabelText += "<color=#ff5e5e>Unable to get Combat Encounter Manager</color>";
            return;
        }

        SerializedProperty sProp;
        if (isPlayer)
        {
            sProp = combatEncounterManager.FindProperty("playerUnits");
        }
        else
        {
            sProp = combatEncounterManager.FindProperty("enemyUnits");
        }


        int arraySize = sProp.arraySize;

        for (int i = 0; i < arraySize; i++)
        {
            if (sProp.GetArrayElementAtIndex(i).objectReferenceValue == unit)
            {
                infoLabelText += "<color=#ff5e5e>Already registered with Combat Encounter Manager</color>";
                return;
            }
        }
        //
        sProp.InsertArrayElementAtIndex(sProp.arraySize);
        SerializedProperty newElement = sProp.GetArrayElementAtIndex(sProp.arraySize - 1);
        newElement.objectReferenceValue = unit;
        combatEncounterManager.ApplyModifiedProperties();
    }
}
