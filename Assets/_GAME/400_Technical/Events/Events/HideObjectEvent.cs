using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CookingChaos.InputEvents
{
    [CreateAssetMenu(fileName = "HideObjectEvent", menuName = RECIPE_EVENT_MENU_NAME + "HideObject Event", order = RECIPE_EVENT_MENU_ORDER)]
    public class HideObjectEvent : RecipeEvent
    {
        #region Fields and Properties
        #endregion

        #region Methods 
        public override void CallEvent(GameObject _targetObject, RecipeInstructionInfo _info){}

        public override void CallEvent(GameObject _targetObject){}

        public override void OnInputPerformed(GameObject _targetObject)
        {
            base.OnInputPerformed(_targetObject);
            _targetObject.gameObject.SetActive(false);
        }
        #endregion
    }
}
