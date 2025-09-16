#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

    [CustomPropertyDrawer(typeof(Tag), true)]
    public sealed class TagDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var registry = FindRegistryAsset();
            var current = property.objectReferenceValue as Tag;

            int count = registry.Tags.Count;
            string[] options = new string[count];
            for (int i = 0; i < count; i++)
                options[i] = registry.Tags[i].TagName;

            int currentIndex = -1;
            for (int i = 0; i < count; i++)
                if (registry.Tags[i] == current) { currentIndex = i; break; }

            int newIndex = EditorGUI.Popup(position, label.text, Mathf.Max(0, currentIndex), options);
            property.objectReferenceValue = registry.Tags[newIndex];
        }

        private static TagRegistry FindRegistryAsset()
        {
            var guid = AssetDatabase.FindAssets("t:TagRegistry")[0];
            var path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath<TagRegistry>(path);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUIUtility.singleLineHeight;
    }
#endif
