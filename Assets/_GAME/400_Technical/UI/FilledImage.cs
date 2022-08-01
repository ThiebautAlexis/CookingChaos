using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace CookingChaos.UI
{
    public class FilledImage : MonoBehaviour
    {
        #region Fields and Properties
        [SerializeField] private Image filledImage = null;
        private static Sequence fillingSequence = null;
        #endregion

        #region Methods
        public void StartFilling(float _startValue, float _endValue, float _duration, float _startingDelay = 0f)
        {
            if (fillingSequence.IsActive())
                fillingSequence.Kill();

            fillingSequence = DOTween.Sequence();
            {
                fillingSequence.AppendInterval(_startingDelay);
                fillingSequence.Append(DOTween.To(SetFillAmount, _startValue, _endValue, _duration));
                fillingSequence.onComplete += StopFill;
            }

            // Local Methods \\ 
            void SetFillAmount(float _value)
            {
                filledImage.fillAmount = _value;
            }
        }

        public void StopFill()
        {
            if (fillingSequence.IsActive())
                fillingSequence.Kill();
        }
        #endregion

    }
}
