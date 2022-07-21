using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookingChaos
{
    [CreateAssetMenu(fileName = "RecipeSettings", menuName = "Cooking Chaos/Recipe Settings", order = 150)]
    public class RecipeSettings : ScriptableObject
    {
        [Header("Complete Settings")]
        public float CompleteInstructionTransitionDuration = .5f;
        public float CompleteRecipeTransitionDuration = .5f;

        [Header("Fail Settings")]
        public float FailInstructionTransitionDuration = .5f;
        public float FailRecipeTransitionDuration = .5f;


    }
}
