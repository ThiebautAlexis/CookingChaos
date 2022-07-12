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
        private int index = 0;
        #endregion

        #region Methods 
        public void Enable()
        {
            index = 0;
            instructions[index].Activate();
        }

        public void OnUpdate()
        {
            if (index == instructions.Length) return;

            int _state = instructions[index].GetInstructionState();
            Debug.Log(_state);
            
            if (_state == 0)
            {
                // Instruction On Hold
            }
            else if (_state == 1)
            {
                instructions[index].Deactivate();
                index++;
                if(index == instructions.Length)
                {
                    // Recipe Complete!
                    return;
                }
                instructions[index].Activate();
                // Instruction Successful
            }
            else if (_state == -1)
            {
                // // Instruction Failed
            }
            
        }
        #endregion
    }
}
