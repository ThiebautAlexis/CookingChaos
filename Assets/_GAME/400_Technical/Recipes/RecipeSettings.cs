using UnityEngine;

namespace CookingChaos
{
    [CreateAssetMenu(fileName = "RecipeSettings", menuName = "Cooking Chaos/Recipe Settings", order = 150)]
    public class RecipeSettings : ScriptableObject
    {
        [Header("Start Stop Settings")]
        public float StartInstructionDelay = .5f;
        public float ActivateInstructionDelay = .5f;

        [Header("Complete Settings")]
        public float CompleteInstructionDuration = .5f;
        public float CompleteRecipeDuration = .5f;

        [Header("Fail Settings")]
        public float FailInstructionDuration = .5f;
        public float FailRecipeDuration = .5f;


    }
}
