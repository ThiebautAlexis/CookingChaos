using System;
using UnityEngine;

namespace CookingChaos
{
    public class RecipeHandler : MonoBehaviour
    {
        #region Fields and Properties
        [SerializeField] private RecipeAsset recipe;
        public static RecipeEventsHandler EventHandler = null;
        #endregion

        #region Methods 

        private void OnEnable()
        {
            RecipeInstruction.AnyKeyInput.Enable();
            EventHandler = recipe.SpawnRecipeEventsHandler();
            recipe.Activate();
        }

        private void OnDisable()
        {
            RecipeInstruction.AnyKeyInput.Disable();
        }
        #endregion
    }
}
