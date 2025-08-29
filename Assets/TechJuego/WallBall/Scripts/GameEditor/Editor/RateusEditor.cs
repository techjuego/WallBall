using TechJuego.Framework.Rateus;
using UnityEditor;
using UnityEngine;

namespace TechJuego.Framework
{
    public class RateusEditor
    {
        private static RateUsData rateSettings;

        // Entry point for the editor window
        public static void ShowRateUs()
        {
            LoadOrCreateRateSettings();
            DisplayRateSettingsUI();
        }

        // Load the settings or create them if they do not exist
        private static void LoadOrCreateRateSettings()
        {
            rateSettings = Resources.Load<RateUsData>("Rateus/RateUsData");

            if (rateSettings == null)
            {
                CreateRateSettings();
                rateSettings = Resources.Load<RateUsData>("Rateus/RateUsData");
            }
        }

        // Displays the settings UI in the editor window
        private static void DisplayRateSettingsUI()
        {
            EditorGUILayout.Space(10);

            DrawAppStoreSection();
            DrawPopupConditionsSection();
            DrawSaveButton();
        }

        // Draw the App Store settings section UI
        private static void DrawAppStoreSection()
        {
            EditorGUILayout.LabelField("App Store IDs", EditorStyles.boldLabel);
            EditorGUILayout.Space(2);

            rateSettings.iosAppID = EditorGUILayout.TextField("iOS App ID", rateSettings.iosAppID, GUILayout.Width(500));
            rateSettings.googlePlayBundleID = EditorGUILayout.TextField("Google Play Bundle ID", rateSettings.googlePlayBundleID, GUILayout.Width(500));

            EditorGUILayout.Space(15);
        }

        // Draw the Popup Conditions settings section UI
        private static void DrawPopupConditionsSection()
        {
            EditorGUILayout.LabelField("Popup Conditions", EditorStyles.boldLabel);
            EditorGUILayout.Space(2);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("When To Show Popup", GUILayout.Width(150));
            rateSettings.WhenToShow = (GameState)EditorGUILayout.EnumPopup(rateSettings.WhenToShow, GUILayout.Width(140));

            GUILayout.Label("||", GUILayout.Width(20));
            GUILayout.Label("Call on every", GUILayout.Width(100));
            rateSettings.CallOnEvery = EditorGUILayout.IntPopup(rateSettings.CallOnEvery,
                new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" },
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                GUILayout.Width(70));

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20);
        }

        // Draw the Save button at the bottom
        private static void DrawSaveButton()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save", GUILayout.Width(100), GUILayout.Height(25)))
            {
                SaveRateSettings();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            EditorGUILayout.Space(20);
        }

        // Save the rate settings to disk
        private static void SaveRateSettings()
        {
            EditorUtility.SetDirty(rateSettings);
            AssetDatabase.SaveAssets();
        }

        // Create a new RateUs settings asset
        private static void CreateRateSettings()
        {
            RateUsData asset = ScriptableObject.CreateInstance<RateUsData>();

            string folderPath = GamePath.ResourcePath + "/Rateus/";
            CreateRateusFolderIfNeeded(folderPath);

            AssetDatabase.CreateAsset(asset, folderPath + "RateUsData.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        // Ensure the Rateus folder exists in the Resources directory
        private static void CreateRateusFolderIfNeeded(string folderPath)
        {
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateFolder(GamePath.ResourcePath, "Rateus");
                AssetDatabase.Refresh();
            }
        }
    }
}
