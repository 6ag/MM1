// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEditor;
using UnityEngine;

namespace Fungus.EditorUtils
{
    [CustomEditor (typeof(EventHandler), true)]
    public class EventHandlerEditor : Editor 
    {
        protected virtual void DrawProperties()
        {
            EditorGUI.indentLevel++;
            SerializedProperty iterator = serializedObject.GetIterator();
            bool enterChildren = true;
            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;

                if (iterator.name == "m_Script")
                {
                    continue;
                }

                EditorGUILayout.PropertyField(iterator, true, new GUILayoutOption[0]);
            }

            EditorGUI.indentLevel--;
        }

        protected virtual void DrawHelpBox()
        {
            EventHandler t = target as EventHandler;
            EventHandlerInfoAttribute info = EventHandlerEditor.GetEventHandlerInfo(t.GetType());
            if (info != null &&
                info.HelpText.Length > 0)
            {
                EditorGUILayout.HelpBox(info.HelpText, MessageType.Info);
            }
        }
        
        #region Public members

        /// <summary>
        /// Returns the class attribute info for an event handler class.
        /// </summary>
        public static EventHandlerInfoAttribute GetEventHandlerInfo(System.Type eventHandlerType)
        {
            object[] attributes = eventHandlerType.GetCustomAttributes(typeof(EventHandlerInfoAttribute), false);
            foreach (var obj in attributes)
            {
                EventHandlerInfoAttribute eventHandlerInfoAttr = obj as EventHandlerInfoAttribute;
                if (eventHandlerInfoAttr != null)
                {
                    return eventHandlerInfoAttr;
                }
            }
            
            return null;
        }

        public virtual void DrawInspectorGUI()
        {
            // Users should not be able to change the MonoScript for the command using the usual Script field.
            // Doing so could cause block.commandList to contain null entries.
            // To avoid this we manually display all properties, except for m_Script.
            serializedObject.Update();

            DrawProperties();
            DrawHelpBox();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
