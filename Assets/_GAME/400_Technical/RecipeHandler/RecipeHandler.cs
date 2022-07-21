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
            recipe.Activate();
        }

        private void OnDisable()
        {
        }
        #endregion
    }
}
