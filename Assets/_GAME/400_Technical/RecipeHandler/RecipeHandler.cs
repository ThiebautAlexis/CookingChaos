using System;
using UnityEngine;

namespace CookingChaos
{
    public class RecipeHandler : MonoBehaviour
    {
        #region Fields and Properties
        [SerializeField] private RecipeAsset recipe;
        [SerializeField] private RecipeEventsHandler handler = null;
        #endregion

        #region Methods 

        private void OnEnable()
        {
            RecipeInstruction.AnyKeyInput.Enable();
            handler = recipe.SpawnRecipeEventsHandler();
            recipe.Activate();
        }

        private void OnDisable()
        {
            RecipeInstruction.AnyKeyInput.Disable();
        }

        private void Update()
        {
            recipe.OnUpdate(handler);
        }
        #endregion
    }
}
