using System;
using UnityEngine;
using DG.Tweening;

namespace CookingChaos.InputEvents
{
    [CreateAssetMenu(fileName = "Move To Position", menuName = RECIPE_EVENT_MENU_NAME + "Move to Position Event", order = RECIPE_EVENT_MENU_ORDER)]

    public class MoveToPositionEvent : RecipeEvent
    {
        #region Fields and Properties
        [SerializeField] private Vector2[] targetPositions = new Vector2[] { };
        [SerializeField] private EventActivation eventActivation = EventActivation.None;
        [SerializeField] private bool overrideSortingOrder = false;
        [SerializeField] private float movementDelay = 0f;
        [SerializeField] private float movementDuration = 1f;
        [SerializeField] private Ease movementEase = Ease.Linear;
        private int currentIndex = 0;
        #endregion

        #region Methods 
        public override void CallEvent(GameObject _targetObject)
        {
            if (currentIndex >= targetPositions.Length) currentIndex = 0;
            if (overrideSortingOrder && _targetObject.TryGetComponent(out SpriteRenderer _renderer))
            {
                _renderer.sortingOrder = currentIndex;
            }

            Sequence movementSequence = DOTween.Sequence();
            {
                movementSequence.AppendInterval(movementDelay);
                movementSequence.Append(_targetObject.transform.DOLocalMove(targetPositions[currentIndex], movementDuration).SetEase(movementEase));
                movementSequence.onComplete += OnSequenceCompleted;
            };
            currentIndex++;

            void OnSequenceCompleted()
            {
                movementSequence.Kill();
            }
        }

        public override void OnInputPerformed(GameObject _targetObject)
        {
            if (eventActivation == EventActivation.OnPerformed)
                CallEvent(_targetObject);
        }

        public override void OnInputCanceled(GameObject _targetObject)
        {
            if (eventActivation == EventActivation.OnCanceled)
                CallEvent(_targetObject);
        }

        public override void OnInputStart(GameObject _targetObject)
        {
            if (eventActivation == EventActivation.OnStarted)
                CallEvent(_targetObject);
        }

        public override void ResetEvent() => currentIndex = 0;
        
        #endregion
    }
}
