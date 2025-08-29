using UnityEngine;
using UnityEditor;
namespace TechJuego.Framework.Utils
{
    public class TakeScreenShot : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("Window/Tech Juego/Take Screen Shot")]
        static void TakeScreen()
        {
            string resolution = "" + Screen.width + "X" + Screen.height;
            ScreenCapture.CaptureScreenshot("ScreenShot-" + resolution + "-" + PlayerPrefs.GetInt("SCREENSHOT", 0) + ".png");
            PlayerPrefs.SetInt("number", PlayerPrefs.GetInt("SCREENSHOT", 0) + 1);
        }
#endif
    }
}