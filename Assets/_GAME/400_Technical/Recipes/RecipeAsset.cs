using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CookingChaos
{
    [CreateAssetMenu(fileName = "Recipe_", menuName = "Cooking Chaos/Recipe", order = 150)]
    public class RecipeAsset : ScriptableObject
    {

        #region static fields
        public static readonly string ReceipeInstructionsPropertyName = "instructions";
        #endregion

        #region Fields and Properties
        
        [SerializeField] private List<RecipeInstruction> instructions = new List<RecipeInstruction>();
        #endregion

        #region Methods 
        [ContextMenu("Instruction/Add Instruction")]
        public void AddInstruction()
        {
            RecipeInstruction _instruction = CreateInstance<RecipeInstruction>();
            _instruction.name = "Recipe Instruction";
            AssetDatabase.AddObjectToAsset(_instruction, this);
            instructions.Add(_instruction);
            AssetDatabase.SaveAssets();
        }
        #endregion
    }
}
