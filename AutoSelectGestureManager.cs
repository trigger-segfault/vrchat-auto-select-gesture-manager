// Auto-select the Gesture Manager GameObject when entering Play Mode to
// quickly test VRChat avatars.

#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TriggerSegfault.editor
{
    /// <summary>
    /// Auto-select the Gesture Manager GameObject when entering Play Mode to
    /// quickly test VRChat avatars.
    /// </summary>
    [InitializeOnLoad]
    public static class AutoSelectGestureManager
    {
        const int ToolsPriority = 0;
        const string AutoSelectEnabledMenuPath = "Tools/Trigger Segfault/Auto-Select Gesture Manager";

        const string PrefsNamespace = "TriggerSegfault/AutoSelectGestureManager/";
        const string AutoSelectEnabledPref = PrefsNamespace + "Enabled";

        /// <summary>
        /// Setting to auto-select or not. The user may want to disable this
        /// during certain circumstances.
        /// </summary>
        public static bool IsAutoSelectEnabled
        {
            get => EditorPrefs.GetBool(AutoSelectEnabledPref, true);
            set => EditorPrefs.SetBool(AutoSelectEnabledPref, value);
        }

        [MenuItem(AutoSelectEnabledMenuPath, false, ToolsPriority)]
        private static void MenuToggleAutoSelectEnabled()
        {
            IsAutoSelectEnabled = !Menu.GetChecked(AutoSelectEnabledMenuPath);
        }

        [MenuItem(AutoSelectEnabledMenuPath, true)]
        private static bool MenuValidateToggleAutoSelectEnabled()
        {
            // Update checked state before showing menu item.
            Menu.SetChecked(AutoSelectEnabledMenuPath, IsAutoSelectEnabled);
            return true; // Menu item is always enabled.
        }

        static AutoSelectGestureManager()
        {
            // Register event to detect when we enter Play Mode, so that we can
            // select Gesture Manager moments after entering.
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
            case PlayModeStateChange.EnteredPlayMode:
                if (IsAutoSelectEnabled)
                {
                    SelectGestureManager();
                }
                break;
            }
        }

        /// <summary>
        /// Finds the first object meeting the conditions of IsGestureManager()
        /// and sets it as the active object.
        /// </summary>
        public static void SelectGestureManager()
        {
            // Look for Gesture Manager in the root of the scene.
            var gestureManager = UnityEngine.SceneManagement.SceneManager
                                            .GetActiveScene()
                                            .GetRootGameObjects()
                                            .Where(IsGestureManager)
                                            .FirstOrDefault();
            if (gestureManager != null)
            {
                // Gesture Manager was found, select it.
                // TODO: Preserve previous selection when returning to editor.
                Selection.activeObject = gestureManager;
            }
        }

        /// <summary>
        /// Tests if the object contains a component named "GestureManager".
        /// </summary>
        public static bool IsGestureManager(GameObject gameObject)
        {
            // Look for a component named "GestureManager" in the object. Do
            // this instead of relying on the Gesture Manager package (to
            // identify the component by type) so that it's not required to have
            // it installed.
            if (gameObject != null && gameObject.activeInHierarchy)
            {
                int count = gameObject.GetComponentCount();
                for (int i = 0; i < count; i++)
                {
                    Component component = gameObject.GetComponentAtIndex(i);
                    if (component.name == "GestureManager")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
#endif
