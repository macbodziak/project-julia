--- Actions:

the Action system consits of the following elements:

Action Manager
	The Action Manager handles selecting player unit, selecting a given action a starting player actions. Selecting the targets for the given action in handles by the Input Manager and depends on the targeting mode of a given action. 

TargetingModeType enum 
	Determines the possible targets for the given action, like one enemy, all enemies, one ally, all allies etc.

Action Behaviour
	This class derives form MonoBehaviour and is attached to the Unit game object. There needs to be one Action Behaviour for one possible action. The Action Behaviour is a wrapper class that holds a reference to an Action Definition (Scriptalbe Object), which in turns holds all the data and executable logic specific to a given action. This class is responsible for starting a given action and holds data such as targets and executing unit which it 	passes to the action definition.

Action Definition
	This Class is a ScriptableObject, which means it can not be directly attached to a GameObject. Thats why a reference to it needs to be provided to the Action Behaviour, which acts as an interface between the Action Definition and the Game Object. 
	Action Definitions hold the data and logic for a specific action. Common Actions are attacks, Healing, Buffs and Debuffs. 
	To Add a new Action Definition, create a class that derives from Action Definition and implement the ExecuteLogic(Unit actingUnit, List<Unit> targets) method.
	To create a new Action Definition, right click right-clicking > Actions > ... in the Editor and choose the template action definition. Populate all proporties (icon sprites are not required for enemy actions) and assign it to an Action Behaviour component.

--- Status Effects:

The Status Effect System consists of the following objects:

StatusEffectController
	This class is responsible for managing a units status effects, that is adding, removing, applying, counting down remaining duration etc.
	Each Unit needs to have a Status Effect Controller attached
	To apply a status effect to a unit, TryReceivingStatusEffect should be invoked 
	To remove a status effect before it is expired, inovoke RemoveStatusEffect
	both methods take a status effect preset (StatusEffect) as parameter which describes the status effect
	invokes events when status effect is applied, removed, succeeded saving throw or immune

StatusEffectBehaviour
	This class in a wrapper for the status effect. It is derived from MonoBehaviour, and as such can easily access and manipulate the game object and its componenets.
	It gets created by the StatusEffectController.
	It needs to be initialized with a StatusEffect, and is reading its data and calling its methods for logic execution;

StatusEffect
	This class represents the actual status effect. Specific status effects need to be implemented by Deriving from this class.
	This class derives from ScriptableObject, thus cannot be directly attached to a fame object, that is what the StatusEffectBehaviour is for.
	As a ScriptableObject, its properties can be easilt set in the editor, making it easy to design and tweak the status effect.
	TO create a new status effect, create a class derived from this class, and overrite the following 
	methods:
		OnStart - here put the logic for starting the effect, like add/removing modifiers etc. Can also be used to create visual effects like particle systems
		OnEnd - here put the logic for ending the effect, mostly to reverse the changes done in the OnStart method
		ApplyEffect - override if you need to perform some logic each turn, like dealing damage
	properties:
		IsActive - should return true, if the status effect needs to be applied each turn, like damage etc.
		Type - should return a value from the StatusEffectType enum. This uniqly identifies a status effect, and a unit can have only one instance of an effect. When creating a new status effect add a coresponding value to the status type enum
	Status Effect object can be created in the Editor via context menu, by right-clicking > Scriptable Objects > Status Effect
		
StatusEffectType
	This enum should have a value for each status effect. 
	It is used to distinquish status effects (make sure we do not apply a status effect a second time but refresh its duration) and also to as a lookup key in tables for Saving Throws etc.

	
--- Creating new Enemies

The UnitSetupHelper class extends the Unity EditorWindow to create a custom editor window that assists in setting up GameObjects as "units" with specific components and properties. 
It provides a user interface with various controls and executes setup operations when a button is clicked.
Unit Setup Helper can be found here: Window>Project Julia> Unit Setup Helper

The Unit Setup Helper configures the selected GameObject by:
    Setting its layer to "Units."
    Adding required components such as Unit, CombatStats, ActionController, StatusEffectController, Animator, and CapsuleCollider.
    Adding specified number of empty ActionBehaviour components.
    Configuring the isPlayer property of the Unit component based on the toggle state.
    Adding a BehaviorTree component for non-player units.
    Adding a SelectedVisual component and its associated prefab as a child.
	
To add a new unit, place a character prefab from synty assets folder, select it, and run the Unit Setup Helper. 
Next, add the unit to the Combat Encounter Manager. The Unit Setup Helper can automate this as well.
You can also do it manually, see steps above.
Add Animator asset to Animator component;
For Enemies, add exisiting or deisgn new Behavior Asset;


--- Working with Behavior Tree
selected targets
selected action
  -- order of the two above
selector
sequence
random selector