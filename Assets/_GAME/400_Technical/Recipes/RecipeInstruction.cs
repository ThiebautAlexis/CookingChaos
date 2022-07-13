using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CookingChaos
{
    public class RecipeInstruction : ScriptableObject
    {
        public static readonly InputAction AnyKeyInput = new InputAction("Any Key", binding: "<Keyboard>/anyKey");

        private static readonly string interaction_Hold = "Hold"; 
        private static readonly string interaction_MultiTap = "MultiTap"; 

        #region Fields and Properties
        public int Index { get; set; }
        [SerializeField] private InputActionMap inputActions;
        private float progress = 0f;
        #endregion

        #region Methods 
        public void Activate()
        {
            progress = 0f;
            inputActions.Enable();
        }

        public void Deactivate()
        {
            inputActions.Disable();
        }

        /// <summary>
        /// Return an int according to the state of the Instruction 
        /// -1 = Failed
        /// 0 = On Hold
        /// 1 = Right Input performed
        /// </summary>
        /// <returns>Return an int according to the state of the Instruction</returns>
        public InstructionState GetInstructionState(out string _eventKey)
        {
            _eventKey = string.Empty;
            for (int i = 0; i < inputActions.actions.Count; i++)
            {
                if (inputActions.actions[i].WasPerformedThisFrame())
                {
                    inputActions.actions[i].Disable();
                    progress += 1f / inputActions.actions.Count;
                    _eventKey = inputActions.actions[i].name;
                    
                    if(progress == 1f)
                    {
                        // Success of all instructions - Callback here
                        return InstructionState.Success;
                    }
                    Debug.Log("Input valid");
                    // Success of this instruction - Callback here 
                    return InstructionState.Input;
                }
                string _interaction = inputActions.actions[i].interactions.Split('(')[0];
                if (_interaction == interaction_Hold && inputActions.actions[i].IsPressed())
                {
                    _eventKey = inputActions.actions[i].name;
                    return InstructionState.Hold;
                }
                if (_interaction == interaction_MultiTap && inputActions.actions[i].WasPressedThisFrame())
                {
                    _eventKey = inputActions.actions[i].name;
                    return InstructionState.MultiTap;
                }
                    
            }
            if (AnyKeyInput.WasPerformedThisFrame())
            {
                // Instructions Failed callback
                return InstructionState.Failed;
            }
            
            return InstructionState.None;
        }
        #endregion
    }
}
