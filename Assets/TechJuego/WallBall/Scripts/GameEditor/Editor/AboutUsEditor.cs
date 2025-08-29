using UnityEngine;
using UnityEditor;

namespace TechJuego.Framework
{
    public class AboutUsEditor
    {
        private static Texture2D techjuegoIcon;

        private static GUIStyle titleStyle;
        private static GUIStyle centerLabelStyle;
        private static GUIStyle linkStyle;
        private static GUIStyle sectionStyle;

        public static void ShowAbout()
        {
            InitStyles();

            if (techjuegoIcon == null)
            {
                techjuegoIcon = Resources.Load<Texture2D>("Graphics/techjuegoIcon");
            }

            GUILayout.BeginVertical("box");
            GUILayout.Space(10);

            // Logo
            if (techjuegoIcon != null)
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label(techjuegoIcon, GUILayout.Width(100), GUILayout.Height(100));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.Space(10);
            }

            // Title
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("TechJuego Framework", titleStyle);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            // Centered "Connect with us:"
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Connect with us:-  techjuego@gmail.com", centerLabelStyle);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(2);

            GUILayout.Space(10);

            DrawButtonRow("Website", "Youtube",
                () => Application.OpenURL("https://techjuego.com"),
                () => Socialmedia.SubscribeOnYoutube());

            DrawButtonRow("Discord", "Twitter",
                () => Socialmedia.ConnectOnDiscord(),
                () => Socialmedia.FollowOnTweeter());

            DrawButtonRow("Facebook", "Instagram",
                () => Socialmedia.FollowOnFacebook(),
                () => Socialmedia.FollowOnInstagram());

            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Open Asset Store Publisher Page", GUILayout.Width(300), GUILayout.Height(30)))
            {
                Application.OpenURL("https://assetstore.unity.com/publishers/46402");
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(15);
            GUILayout.EndVertical();
        }

        private static void DrawButtonRow(string label1, string label2, System.Action action1, System.Action action2)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(label1, GUILayout.Width(150), GUILayout.Height(25)))
                action1?.Invoke();
            if (GUILayout.Button(label2, GUILayout.Width(150), GUILayout.Height(25)))
                action2?.Invoke();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
        }

        private static void InitStyles()
        {
            if (titleStyle == null)
            {
                titleStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 16,
                    alignment = TextAnchor.MiddleCenter
                };
            }

            if (centerLabelStyle == null)
            {
                centerLabelStyle = new GUIStyle(EditorStyles.label)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontStyle = FontStyle.Bold,
                    fontSize = 12
                };
            }

            if (linkStyle == null)
            {
                linkStyle = new GUIStyle(EditorStyles.label)
                {
                    normal = { textColor = new Color(0.2f, 0.6f, 1f) }
                };
            }

            if (sectionStyle == null)
            {
                sectionStyle = new GUIStyle(EditorStyles.label)
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 12
                };
            }
        }
    }
}
