using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CookingChaos.Recipe.Editor
{
    [CustomEditor(typeof(RecipeAsset))]
    public class RecipeAssetEditor : UnityEditor.Editor
    {
        #region Fields and Properties
        public VisualTreeAsset recipeAssetInspector_UXML;

        private SerializedProperty recipeInstructionsProperty;
        private SerializedProperty eventsHandlerProperty;
        private SerializedProperty scoreProperty;
        private SerializedProperty settingsProperty;
        private ListView listView;
        #endregion

        #region Methods 
        private void OnEnable()
        {
            recipeInstructionsProperty = serializedObject.FindProperty(RecipeAsset.ReceipeInstructionsPropertyName);
            eventsHandlerProperty = serializedObject.FindProperty(RecipeAsset.EventsHandlerPropertyName);
            scoreProperty = serializedObject.FindProperty(RecipeAsset.ScorePropertyName);
            settingsProperty = serializedObject.FindProperty(RecipeAsset.SettingsPropertyName);
        }


        public override VisualElement CreateInspectorGUI()
        {
            // Create an empty visual Element and clone the default UMXL style. 
            VisualElement _inspectorContainer = new VisualElement();
            recipeAssetInspector_UXML.CloneTree(_inspectorContainer);
            
            // Instructions
            ListView _list = _inspectorContainer.Q<ListView>("list-view");
            _list.fixedItemHeight = EditorGUIUtility.singleLineHeight;

            _list.makeItem = OnMakeListViewItem;
            _list.itemsAdded += OnItemsAdded;
            _list.BindProperty(recipeInstructionsProperty);
            listView = _list;

            // Event Handler
            PropertyField _propertyField = _inspectorContainer.Q<PropertyField>("events-handler");
            _propertyField.BindProperty(eventsHandlerProperty);

            // Score
            IntegerField _scoreField = _inspectorContainer.Q<IntegerField>("score");
            _scoreField.BindProperty(scoreProperty);

            // Settings
            PropertyField _settingsField = _inspectorContainer.Q<PropertyField>("settings");
            _settingsField.BindProperty(settingsProperty);

            return _inspectorContainer;
        }

        private void OnItemsAdded(IEnumerable<int> obj)
        {
            List<int> _objects = new List<int>(obj);
            int _index = _objects[0];

            RecipeInstruction _instruction = CreateInstance<RecipeInstruction>();
            _instruction.name = $"{serializedObject.targetObject.name} Instruction {_index.ToString("000")}";
            _instruction.Index = _index;
            AssetDatabase.AddObjectToAsset(_instruction, serializedObject.targetObject);
            
            recipeInstructionsProperty.GetArrayElementAtIndex(_index).objectReferenceValue = _instruction;
            serializedObject.ApplyModifiedProperties();

            AssetDatabase.SaveAssets();
        }

        private VisualElement OnMakeListViewItem()
        {
            ObjectField _item = new ObjectField();
            _item.objectType = typeof(RecipeInstruction);
            _item.allowSceneObjects = false;

            _item.AddManipulator(new ContextualMenuManipulator((ContextualMenuPopulateEvent evt) => 
            {
                evt.menu.AppendAction("Select Instruction", SelectInstruction);
                evt.menu.AppendAction("Delete Instruction", DeleteInstruction);

            } ));
            
            void SelectInstruction(DropdownMenuAction evt) => EditorGUIUtility.PingObject(_item.value);
            void DeleteInstruction(DropdownMenuAction evt)
            {
                RecipeInstruction _instruction = _item.value as RecipeInstruction;
                if(_instruction != null)
                {
                    recipeInstructionsProperty.DeleteArrayElementAtIndex(_instruction.Index);
                    serializedObject.ApplyModifiedProperties();
                    AssetDatabase.RemoveObjectFromAsset(_instruction);
                    AssetDatabase.SaveAssets();
                }
            }

            return _item;
        }
        #endregion
    }
}
