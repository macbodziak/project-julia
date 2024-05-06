Actions:
The Action Manager handles selecting lplayer unit, selecting a given action and selecting the target for the action. The specific behaviour depends on action type, which in turn determines the input state type that is set in the input manager

To create a new Action:
1. create a Scritpalbe Object to hold data sepcific to this action derived from BaseActionData
create a class that inherits from BaseAction
override the ExecuteLogic() method

Status Effects:

The Unit Game Object has to have a StatusEffectController Attached, which handles status effects
Status Effects are added by calling the ReceiveStatusEffect<T>() method, where T is the Type of status effect, which adds the specified status effect component
Status Effects can be removed by calling RemoveStatusEffect<T>() for a specific status effect or Clear() to remove all at once
only one instance of a given type can be attached to a unit game object
To create a new status effect:
1. create a Scritpalbe Object to hold data sepcific to this effect derived from BaseStatusEffectData
2. create a class derived from this class and implemet its ApplyEffect methodand add effect specific data and behaviour
3. override the IsAppliedEachTurn() method to return true, if the effect shouldbe apllied each turn, like do damage each turn
4. call the LoadData method in the derived Awake method with the path to the  Scriptable Object containing data for the specific status effect