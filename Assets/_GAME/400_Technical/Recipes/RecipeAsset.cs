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
        public static readonly string EventsHandlerPropertyName = "eventsHandler";
        #endregion

        #region Fields and Properties
        [SerializeField] private RecipeInstruction[] instructions = new RecipeInstruction[] { };
        [SerializeField] private RecipeEventsHandler eventsHandler = null; 
        private int index = 0;
        private string eventKey = string.Empty;
        #endregion

        #region Methods
        public RecipeEventsHandler SpawnRecipeEventsHandler() => Instantiate(eventsHandler);


        public void Activate()
        {
            index = 0;
            instructions[index].Activate();
        }

        /*
        public void OnUpdate(RecipeEventsHandler _spawnedHandler)
        {
            if (index == instructions.Length) return;

            InstructionState _state = instructions[index].GetInstructionState(out eventKey, out float _holdingProgress, out int _multitapCount);

            switch (_state)
            {
                case InstructionState.Failed:
                    _spawnedHandler.FailedInstructionEvent();
                    break;
                case InstructionState.Success:
                    _spawnedHandler.ValidInputEvent(eventKey);
                    OnInstructionSucceded();
                    break;
                case InstructionState.Input:
                    _spawnedHandler.ValidInputEvent(eventKey);
                    break;
                case InstructionState.MultiTap:
                    _spawnedHandler.MultiTapInputEvent(eventKey, _multitapCount);
                    break;
                case InstructionState.Hold:
                    _spawnedHandler.HoldingInputEvent(eventKey, _holdingProgress);
                    break;
                default:
                    break;
            }            
        }
        */

        private void OnInstructionSucceded(RecipeEventsHandler _eventsHandler)
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
