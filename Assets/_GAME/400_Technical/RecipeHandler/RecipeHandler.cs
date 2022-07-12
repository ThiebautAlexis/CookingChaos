using System;
using UnityEngine;

namespace CookingChaos
{
    public class RecipeHandler : MonoBehaviour
    {
        #region Fields and Properties
        [SerializeField] private RecipeAsset recipe;
        #endregion

        #region Methods 

        private void OnEnable()
        {
            RecipeInstruction.AnyKeyInput.Enable();
            recipe.Enable();
        }

        private void OnDisable()
        {
            RecipeInstruction.AnyKeyInput.Disable();
        }

        private void Update()
        {
            recipe.OnUpdate();
        }
        #endregion
    }
}
