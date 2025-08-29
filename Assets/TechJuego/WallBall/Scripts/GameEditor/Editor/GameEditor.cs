using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace TechJuego.Framework
{
#if UNITY_EDITOR
    [InitializeOnLoad]
    public class GameEditor : EditorWindow
    {
        static GameEditor()
        {
        }
        private Vector2 scrollViewVector;
        private static int selected;
        private readonly string[] toolbarStrings = { "Sound",  "Ads",  "Rate us", "About" };
        private static GameEditor window;
        [MenuItem("Window/Tech Juego/Game editor and settings")]
        public static void Init()
        {
            // Get existing open window or if none, make a new one:
            window = (GameEditor)GetWindow(typeof(GameEditor), false, "Game editor");
            window.Show();
        }
        private static GUIStyle tabButtonStyle;
        // Initialize custom GUI styles for toolbar and tabs
        private static void InitializeToolbarStyles()
        {
            // Style for each Tab Button
            tabButtonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 14,
                normal = { textColor = Color.white, background = Texture2D.whiteTexture }, // Transparent background
                hover = { textColor = Color.yellow },
                alignment = TextAnchor.MiddleCenter,
                padding = new RectOffset(20, 20, 5, 5), // Adds padding for better button size
                margin = new RectOffset(5, 5, 5, 5)
            };
        }

        private void OnGUI()
        {
            InitializeToolbarStyles();

            GUI.changed = false;
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            int oldSelected = selected;
            selected = GUILayout.Toolbar(selected, toolbarStrings, tabButtonStyle, GUILayout.Width(700));
            GUILayout.EndHorizontal();
            scrollViewVector = GUI.BeginScrollView(new Rect(0, 45, position.width, position.height), scrollViewVector, new Rect(0, 0, 600, 1600));
            GUILayout.Space(-30);
            if (oldSelected != selected)
                scrollViewVector = Vector2.zero;
      
            if (toolbarStrings[selected] == "Sound")
            {
                SoundEditor.ShowSound();
            }
            if (toolbarStrings[selected] == "Ads")
            {
#if UNITY_WEBGL
                WebglAdsEditor.ShowMonetization();
#endif
#if UNITY_ANDROID || UNITY_IPHONE
                MobileAdsEditor.ShowMonetization();
#endif
            }
            if (toolbarStrings[selected] == "Rate us")
            {
                RateusEditor.ShowRateUs();
            }
            if (toolbarStrings[selected] == "About")
            {
                AboutUsEditor.ShowAbout();
            }
            GUI.EndScrollView();
            if (GUI.changed && !EditorApplication.isPlaying)
                EditorSceneManager.MarkAllScenesDirty();
        }
    }
#endif
}