using System;
using UnityEngine;

namespace CookingChaos.UI
{
    public class UIManager : MonoBehaviour
    {
        #region Fields and Properties
        [Header("In Game UI")]
        [SerializeField] private FilledImage instructionDurationGauge = null;
        #endregion

        #region Methods
        private void OnEnable()
        {
            RecipeAsset.OnInstructionStarted += SetinstructionDurationGauge;
            RecipeAsset.OnInstructionCompleted += StopInstructionDurationGauge;
            RecipeAsset.OnInstructionFailed += StopInstructionDurationGauge;
        }

        private void OnDisable()
        {
            RecipeAsset.OnInstructionStarted -= SetinstructionDurationGauge;
            RecipeAsset.OnInstructionCompleted -= StopInstructionDurationGauge;
            RecipeAsset.OnInstructionFailed -= StopInstructionDurationGauge;
        }

        private void SetinstructionDurationGauge(RecipeInstructionInfo _info)
        {
            instructionDurationGauge.StartFilling(1f, 0f, _info.Duration, _info.BeforeStartDelay + _info.BeforeActivationDelay);
        }
        private void StopInstructionDurationGauge(int _index = 0)
        {
            instructionDurationGauge.StopFill();
        }

        #endregion
    }
}
