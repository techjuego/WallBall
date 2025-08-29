using System.Collections;
using System.Collections.Generic;
using TechJuego.Framework.Sound;
using UnityEditor;
using UnityEngine;

namespace TechJuego.Framework
{
    public class SoundEditor
    {
        private static SoundsHolder soundsHolder;
        private static GUIStyle boldLabelStyle;
        private static GUIStyle buttonStyle;
        private static GUIStyle boxStyle;

        // Initialize custom GUI styles
        private static void InitializeGUIStyles()
        {
            // Bold Label Style
            boldLabelStyle = new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 14,
                normal = { textColor = Color.white }
            };

            // Button Style (no white texture background)
            buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 12,
                normal = { textColor = Color.white },
                hover = { textColor = Color.gray },
                alignment = TextAnchor.MiddleCenter,
                padding = new RectOffset(10, 10, 5, 5) // Adjust padding for a better look
            };

            // Box Style for section titles and grouping
            boxStyle = new GUIStyle(GUI.skin.box)
            {
                fontSize = 12,
                normal = { textColor = Color.white },
                padding = new RectOffset(10, 10, 10, 10)
            };
        }

        // Entry point to show Sound editor
        public static void ShowSound()
        {
            if (soundsHolder == null)
            {
                soundsHolder = Resources.Load("Sounds/SoundsHolder") as SoundsHolder;
                if (soundsHolder == null)
                {
                    CreateSoundSettings();
                    soundsHolder = Resources.Load("Sounds/SoundsHolder") as SoundsHolder;
                }
            }

            // Initialize GUI Styles
            InitializeGUIStyles();

            if (soundsHolder != null)
            {
                GUILayout.BeginVertical(boxStyle);
                GUILayout.Label("SFX", boldLabelStyle);

                // Display SFX sound clips
                for (int i = 0; i < soundsHolder.soundClips.Count; i++)
                {
                    DisplaySoundClip(i, soundsHolder.soundClips);
                }

                // Add SFX sound button
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Add Sound", buttonStyle, GUILayout.Width(100)))
                {
                    soundsHolder.soundClips.Add(new SoundClips());
                    EditorUtility.SetDirty(soundsHolder);
                    AssetDatabase.SaveAssets();
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(20); // Space between sections

                // Display Music sound clips
                GUILayout.Label("Music", boldLabelStyle);
                for (int i = 0; i < soundsHolder.musicClip.Count; i++)
                {
                    DisplaySoundClip(i, soundsHolder.musicClip);
                }

                // Add Music sound button
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Add Music", buttonStyle, GUILayout.Width(100)))
                {
                    soundsHolder.musicClip.Add(new SoundClips());
                    EditorUtility.SetDirty(soundsHolder);
                    AssetDatabase.SaveAssets();
                }
                GUILayout.EndHorizontal();

                // Save Button
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Save", buttonStyle, GUILayout.Width(80)))
                {
                    EditorUtility.SetDirty(soundsHolder);
                    AssetDatabase.SaveAssets();
                }
                GUILayout.EndHorizontal();

                GUILayout.EndVertical();
            }
        }

        // Helper method to display individual SoundClip details (both SFX and Music)
        private static void DisplaySoundClip(int index, List<SoundClips> soundClipsList)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label((index + 1) + ".", GUILayout.Width(30));

            // Clip name
            GUILayout.Label("Clip name", GUILayout.Width(80));
            soundClipsList[index].clipName = EditorGUILayout.TextField("", soundClipsList[index].clipName, GUILayout.Width(150));

            // Clip audio
            GUILayout.Label("Clip", GUILayout.Width(60));
            soundClipsList[index].clip = (AudioClip)EditorGUILayout.ObjectField("", soundClipsList[index].clip, typeof(AudioClip), false, GUILayout.Width(100));

            // Volume control
            GUILayout.Label("Volume", GUILayout.Width(60));
            soundClipsList[index].volume = EditorGUILayout.Slider(soundClipsList[index].volume, 0, 1, GUILayout.Width(150));

            // Remove button
            if (GUILayout.Button(new GUIContent("X", "X"), GUILayout.Width(30)))
            {
                soundClipsList.RemoveAt(index);
                EditorUtility.SetDirty(soundsHolder);
                AssetDatabase.SaveAssets();
            }

            GUILayout.Space(Screen.width);
            GUILayout.EndHorizontal();
        }

        // Create the SoundsHolder asset if it doesn't exist
        private static void CreateSoundSettings()
        {
            SoundsHolder asset = ScriptableObject.CreateInstance<SoundsHolder>();
            if (!AssetDatabase.IsValidFolder(GamePath.ResourcePath + "/Sounds/"))
            {
                AssetDatabase.CreateFolder(GamePath.ResourcePath, "Sounds");
                AssetDatabase.Refresh();
            }
            AssetDatabase.CreateAsset(asset, GamePath.ResourcePath + "/Sounds/SoundsHolder.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
