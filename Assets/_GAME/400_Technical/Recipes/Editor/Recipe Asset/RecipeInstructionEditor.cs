using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CookingChaos.Recipe.Editor
{
    [CustomEditor(typeof(RecipeInstruction))]
    public class RecipeInstructionEditor : UnityEditor.Editor
    {

        // private static readonly string customDrawerPath = "Assets/_GAME/400_Technical/Recipes/Editor/Custom UI Controllers/InputIntervalDrawer.uxml";
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement _container = new VisualElement();
            InspectorElement.FillDefaultInspector(_container, serializedObject, this);
            /*
            MinMaxSlider _slider = new MinMaxSlider("Valid Interval", _intervalProperty.vector2Value.x, _intervalProperty.vector2Value.y, 0f, 1f);
            _slider.BindProperty(_intervalProperty);
            _container.Add(_slider);
            */

            InputIntervalDrawer _drawer = new InputIntervalDrawer();
            SerializedProperty _intervalProperty = serializedObject.FindProperty(RecipeInstruction.IntervalPropertyName);
            SerializedProperty _durationProperty = serializedObject.FindProperty(RecipeInstruction.DurationPropertyName);
            

            _drawer.BindProperty(_intervalProperty);
            _container.Add(_drawer);

            return _container;
        }
    }
}
