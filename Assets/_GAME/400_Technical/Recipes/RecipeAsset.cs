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
        private RecipeEventsHandler spawnedEventsHandler = null;
        #endregion

        #region Methods
        public void Activate()
        {
            spawnedEventsHandler = Instantiate(eventsHandler);
            index = 0;
            RecipeInstruction.OnInstructionSucceded += OnInstructionSucceded;
            RecipeInstruction.OnInstructionFailed += OnInstructionFailed;

            instructions[index].Activate();
        }

        private void OnInstructionSucceded()
        {
            instructions[index].Deactivate();
            spawnedEventsHandler.CallSuccessInstructionEvent(index);
            index++;

            if (index >= instructions.Length)
            {
                // Recipe Complete!
                RecipeInstruction.OnInstructionSucceded -= OnInstructionSucceded;
                Destroy(spawnedEventsHandler);
                return;
            }
            instructions[index].Activate();
        }

        private void OnInstructionFailed()
        {
            Debug.Log("Failed!");
            RecipeInstruction.OnInstructionFailed -= OnInstructionSucceded;
        }
        #endregion

    }
}
