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
        private ListView listView;
        #endregion

        #region Methods 
        private void OnEnable()
        {
            recipeInstructionsProperty = serializedObject.FindProperty(RecipeAsset.ReceipeInstructionsPropertyName);
        }


        public override VisualElement CreateInspectorGUI()
        {
            // Create an empty visual Element and clone the default UMXL style. 
            VisualElement _inspectorContainer = new VisualElement();
            recipeAssetInspector_UXML.CloneTree(_inspectorContainer);

            ListView _list = _inspectorContainer.Q<ListView>("list-view");
            _list.fixedItemHeight = EditorGUIUtility.singleLineHeight;

            _list.makeItem = OnMakeListViewItem;
            _list.itemsAdded += OnItemsAdded;
            _list.BindProperty(recipeInstructionsProperty);
            listView = _list;

            _inspectorContainer.Add(_list);

            return _inspectorContainer;
        }

        private void OnItemsAdded(IEnumerable<int> obj)
        {
            List<int> _objects = new List<int>(obj);
            int _index = _objects[0];

            RecipeInstruction _instruction = CreateInstance<RecipeInstruction>();
            _instruction.name = $"{serializedObject.targetObject.name} Instruction {_index.ToString("000")}";
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
                UnityEngine.Object _object = _item.value;
                
                AssetDatabase.RemoveObjectFromAsset(_object);
                AssetDatabase.SaveAssets();
            }

            return _item;
        }
        #endregion
    }
}
