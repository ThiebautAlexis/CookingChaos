using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.InputSystem;

namespace CookingChaos
{
    [CreateAssetMenu(fileName = "Recipe_", menuName = "Cooking Chaos/Recipe", order = 150)]
    public class RecipeAsset : ScriptableObject
    {

        #region static fields
        public static readonly string ReceipeInstructionsPropertyName = "instructions";
        public static readonly string EventsHandlerPropertyName = "eventsHandler";
        public static readonly string ScorePropertyName = "score";
        public static readonly string SettingsPropertyName = "settings";

        // private readonly string AnyKeyName = "AnyKey";
        // private readonly string AnyKeyBinding = "<Keyboard>/anyKey";
        #endregion

        // public static InputAction AnyKeyInput;

        public static event Action<float> OnRecipeCompleted;
        public static event Action OnRecipeFailed;
        public static event Action<int> OnInstructionCompleted;
        public static event Action<int> OnInstructionFailed;

        #region Fields and Properties
        [SerializeField] private RecipeInstruction[] instructions = new RecipeInstruction[] { };
        [SerializeField] private RecipeEventsHandler eventsHandler = null; 
        private int index = 0;

        [SerializeField] private int score = 100;
        [SerializeField] private RecipeSettings settings;
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

        /*
        private void ResetInputs()
        {
            if (AnyKeyInput != null && AnyKeyInput.enabled) 
                AnyKeyInput.Disable();

            AnyKeyInput = new InputAction(AnyKeyName, binding: AnyKeyBinding);
            AnyKeyInput.performed += OnAnyKeyPerformed;

            instructions[index].Activate();
            AnyKeyInput.Enable();
        }
        */

        private void CompleteInstruction()
        {
            instructions[index].Deactivate();

            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(settings.CompleteInstructionTransitionDuration);
            sequence.AppendCallback(CompleteInstructionCallback);

            void CompleteInstructionCallback()
            {
                OnInstructionCompleted?.Invoke(index);
                index++;
                if (index >= instructions.Length)
                {
                    // Recipe Complete!
                    CompleteRecipe();
                    return;
                }
                instructions[index].Activate();
            }
        }

        private void CompleteRecipe()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(settings.CompleteRecipeTransitionDuration);
            sequence.AppendCallback(CompleteRecipeCallback);

            void CompleteRecipeCallback()
            {
                OnRecipeCompleted?.Invoke(score);
                Deactivate();
            }
        }

        private void FailInstruction()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(settings.FailInstructionTransitionDuration);
            sequence.AppendCallback(InstructionFailedCallback);
            sequence.AppendInterval(settings.FailRecipeTransitionDuration);
            sequence.AppendCallback(RecipeFailedCallback);

            void InstructionFailedCallback() => OnInstructionFailed?.Invoke(index);
            void RecipeFailedCallback()
            {
                OnRecipeFailed?.Invoke();
                Deactivate();
            }
        }
        #endregion

    }
}
