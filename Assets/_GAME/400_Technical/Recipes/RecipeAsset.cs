using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace CookingChaos
{
    [CreateAssetMenu(fileName = "Recipe_", menuName = "Cooking Chaos/Recipe", order = 150)]
    public class RecipeAsset : ScriptableObject
    {

        #region static fields
        public static readonly string ReceipeInstructionsPropertyName = "instructions";
        #endregion

        #region Fields and Properties
        [SerializeField] private RecipeInstruction[] instructions = new RecipeInstruction[] { };
        [SerializeField] private RecipeEventsHandler eventsHandler = null; 
        private int index = 0;
        private string eventKey = string.Empty;
        #endregion

        #region Methods 
        public void Activate()
        {
            Instantiate(eventsHandler);
            index = 0;
            instructions[index].Activate();
        }

        public void OnUpdate()
        {
            if (index == instructions.Length) return;

            InstructionState _state = instructions[index].GetInstructionState(out eventKey);

            switch (_state)
            {
                case InstructionState.Failed:
                    OnInstructionFailed();
                    break;
                case InstructionState.Success:
                    OnValidInput(eventKey);
                    OnInstructionSucceded();
                    break;
                case InstructionState.Input:
                    OnValidInput(eventKey);
                    break;
                case InstructionState.MultiTap:
                    OnMultiTapInput(eventKey);
                    break;
                case InstructionState.Hold:
                    OnHoldInput(eventKey);
                    break;
                default:
                    break;
            }            
        }

        private void OnInstructionFailed()
        {
            eventsHandler.FailedInstructionEvent();
        }

        private void OnValidInput(string eventKey)
        {
            Debug.Log("Valid Input => " + eventKey);
            eventsHandler.ValidInputEvent(eventKey);
        }

        private void OnHoldInput(string eventKey)
        {
            Debug.Log("Holding " + eventKey);
            eventsHandler.HoldingInputEvent(eventKey);
        }

        private void OnMultiTapInput(string eventKey)
        {
            Debug.Log("MultiTap " + eventKey);
            eventsHandler.MultiTapInputEvent(eventKey);
        }

        private void OnInstructionSucceded()
        {
            instructions[index].Deactivate();
            index++;
            if (index == instructions.Length)
            {
                // Recipe Complete!
                Debug.Log("recipe completed");
                return;
            }
            Debug.Log("Proceed to next instruction");
            instructions[index].Activate();
        }
        #endregion

    }

    public enum InstructionState
    {
        Failed = -1,
        None,
        Success,
        Input,
        MultiTap,
        Hold
    }
}
