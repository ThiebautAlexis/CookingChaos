using UnityEngine;
using System;
using DG.Tweening;

namespace CookingChaos
{
    [CreateAssetMenu(fileName = "Recipe_", menuName = "Cooking Chaos/Recipe", order = 150)]
    public class RecipeAsset : ScriptableObject
    {

        #region static fields
        public static readonly string ReceipeInstructionsPropertyName = "instructions";
        public static readonly string EventsHandlerPropertyName = "eventsHandler";

        private float completionInterval = 1.5f;
        #endregion

        public static event Action OnRecipeCompleted;
        public static event Action<int> OnInstructionCompleted;
        public static event Action<int> OnInstructionFailed;

        #region Fields and Properties
        [SerializeField] private RecipeInstruction[] instructions = new RecipeInstruction[] { };
        [SerializeField] private RecipeEventsHandler eventsHandler = null; 
        private int index = 0;
        #endregion

        #region Methods
        public void Activate()
        {
            Instantiate(eventsHandler);
            index = 0;
            RecipeInstruction.OnInstructionSucceded += CompleteInstruction;
            RecipeInstruction.OnInstructionFailed += FailInstruction;
            instructions[index].Activate();
        }

        public void Deactivate()
        {
            RecipeInstruction.OnInstructionSucceded -= CompleteInstruction;
            RecipeInstruction.OnInstructionFailed -= FailInstruction;
        }

        private void CompleteInstruction()
        {
            instructions[index].Deactivate();
            OnInstructionCompleted?.Invoke(index);

            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(completionInterval);
            sequence.onComplete += GoToNextInstruction;

            void GoToNextInstruction()
            {
                index++;
                if (index >= instructions.Length)
                {
                    // Recipe Complete!
                    OnRecipeCompleted?.Invoke();
                    Deactivate();
                    return;
                }
                instructions[index].Activate();
            }
        }

        private void FailInstruction()
        {
            Debug.Log("Failed!");
            OnInstructionFailed?.Invoke(index);
            Deactivate();
        }
        #endregion

    }
}
