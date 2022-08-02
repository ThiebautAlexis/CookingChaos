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
        public static event Action<RecipeInstructionInfo> OnInstructionStarted;
        public static event Action<int> OnInstructionCompleted;
        public static event Action<int> OnInstructionFailed;

        #region Fields and Properties
        [SerializeField] private RecipeInstruction[] instructions = new RecipeInstruction[] { };
        [SerializeField] private RecipeEventsHandler eventsHandler = null; 
        private int index = 0;

        [SerializeField] private int score = 100;
        [SerializeField] private RecipeSettings settings;

        private static Sequence transitionSequence = null;
        #endregion

        #region Methods
        public void Activate()
        {
            Instantiate(eventsHandler);
            index = 0;
            RecipeInstruction.OnInstructionSucceded += CompleteInstruction;
            RecipeInstruction.OnInstructionFailed += FailInstruction;
            StartInstruction();
        }

        public void Deactivate()
        {
            RecipeInstruction.OnInstructionSucceded -= CompleteInstruction;
            RecipeInstruction.OnInstructionFailed -= FailInstruction;
            transitionSequence.Kill();
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

        private void StartInstruction()
        {
            if (transitionSequence.IsActive())
                transitionSequence.Kill(true);
            transitionSequence = DOTween.Sequence();
            {
                transitionSequence.AppendInterval(settings.StartInstructionDelay);
                transitionSequence.AppendCallback(StartInstructionCallback);
                transitionSequence.AppendInterval(settings.ActivateInstructionDelay);
                transitionSequence.AppendCallback(ActivateInstruction);
            }

            // Local Methods \\
            void StartInstructionCallback()
            {
                RecipeInstructionInfo _info = new RecipeInstructionInfo()
                {
                    Index = index,
                    BeforeStartDelay = settings.StartInstructionDelay,
                    BeforeActivationDelay = settings.ActivateInstructionDelay,
                    Duration = instructions[index].Duration
                };  
                OnInstructionStarted?.Invoke(_info);
            }

            void ActivateInstruction()
            {
                instructions[index].Activate();
            }
        }

        private void CompleteInstruction()
        {
            instructions[index].Deactivate();

            transitionSequence = DOTween.Sequence();
            transitionSequence.AppendInterval(settings.CompleteInstructionDuration);
            transitionSequence.AppendCallback(CompleteInstructionCallback);

            void CompleteInstructionCallback()
            {
                OnInstructionCompleted?.Invoke(index);
                index++;
                if (index >= instructions.Length)
                    CompleteRecipe();
                else
                    StartInstruction();
            }
        }

        private void CompleteRecipe()
        {
            if (transitionSequence.IsActive())
                transitionSequence.Kill(true);

            transitionSequence = DOTween.Sequence();
            transitionSequence.AppendInterval(settings.CompleteRecipeDuration);
            transitionSequence.AppendCallback(CompleteRecipeCallback);

            void CompleteRecipeCallback()
            {
                OnRecipeCompleted?.Invoke(score);
                Deactivate();
            }
        }

        private void FailInstruction()
        {
            transitionSequence = DOTween.Sequence();
            transitionSequence.AppendInterval(settings.FailInstructionDuration);
            transitionSequence.AppendCallback(InstructionFailedCallback);
            transitionSequence.AppendInterval(settings.FailRecipeDuration);
            transitionSequence.AppendCallback(RecipeFailedCallback);

            void InstructionFailedCallback() => OnInstructionFailed?.Invoke(index);
            void RecipeFailedCallback()
            {
                OnRecipeFailed?.Invoke();
                Deactivate();
            }
        }
        #endregion

    }

    public struct RecipeInstructionInfo
    {
        public int Index;
        public float BeforeStartDelay;
        public float BeforeActivationDelay;
        public float Duration;

    }
}
