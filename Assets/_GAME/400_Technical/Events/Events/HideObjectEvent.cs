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
        public override void OnInputPerformed(SpriteRenderer _renderer)
        {
            base.OnInputPerformed(_renderer);
            _renderer.gameObject.SetActive(false);
        }
        #endregion
    }
}
