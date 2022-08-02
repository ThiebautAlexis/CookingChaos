using UnityEngine;
using DG.Tweening;

namespace CookingChaos.InputEvents
{
    [CreateAssetMenu(fileName = "Rotate Event", menuName = RECIPE_EVENT_MENU_NAME + "Rotate Event", order = RECIPE_EVENT_MENU_ORDER)]
    public class RotateObjectEvent : RecipeEvent
    {
        #region Fields and Properties
        [SerializeField] private EventActivation eventActivation = EventActivation.None;
        [SerializeField] private float rotationDelay = 0f;
        [SerializeField] private float rotationDuration = 1f;
        [SerializeField] private int rotationCount = 1;
        #endregion 

        #region Methods
        public override void CallEvent(GameObject _targetObject)
        {
            RecipeInstructionInfo _info = new RecipeInstructionInfo
            {
                Index = 0,
                BeforeStartDelay = rotationDelay,
                BeforeActivationDelay = rotationDuration
            };
            CallEvent(_targetObject, _info);
        }

        public override void CallEvent(GameObject _targetObject, RecipeInstructionInfo _info)
        {

            Sequence movementSequence = DOTween.Sequence();
            {
                movementSequence.AppendInterval(_info.BeforeStartDelay);
                movementSequence.Append(_targetObject.transform.DORotate(Vector3.forward * 360 * rotationCount, _info.BeforeActivationDelay, RotateMode.LocalAxisAdd));
                movementSequence.onComplete += OnSequenceCompleted;
            };

            void OnSequenceCompleted()
            {
                movementSequence.Kill();
            }
        }
        #endregion
    }
}
