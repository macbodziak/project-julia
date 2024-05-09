Actions:
The Action Manager handles selecting lplayer unit, selecting a given action and selecting the target for the action. The specific behaviour depends on action type, which in turn determines the input state type that is set in the input manager

To create a new Action:
1. create a Scritpalbe Object to hold data sepcific to this action derived from BaseActionData
create a class that inherits from BaseAction
override the ExecuteLogic() method

--- Status Effects:

The Status Effect System consists of the following objects:

StatusEffectController
	This class is responsible for managing a units status effects, that is adding, removing, applying, counting down remaining duration etc.
	Each Unit needs to have a Status Effect Controller attached
	To apply a status effect to a unit, TryReceivingStatusEffect should be invoked 
	To remove a status effect before it is expired, inovoke RemoveStatusEffect
	both methods take a status effect preset (StatusEffect) as parameter which describes the status effect

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
		
StatusEffectType
	This enum should have a value for each status effect. 
	It is used to distinquish status effects (make sure we do not apply a status effect a second time but refresh its duration) and also to as a lookup key in tables for Saving Throws etc.