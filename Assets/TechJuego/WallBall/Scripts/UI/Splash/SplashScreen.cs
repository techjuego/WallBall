using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TechJuego.Framework.Utils;
using System.Collections;
using UnityEngine.SceneManagement;

namespace TechJuego.Framework
{
    public class SplashScreen : MonoBehaviour
    {
        // Time To wait Afer Scene is Loaded
        [SerializeField] private float time;

        private AsyncOperation scene;
        //Loading Progress Text
        [SerializeField] private TextMeshProUGUI m_LoadingText;
        [SerializeField] private Slider m_LoadinImage;

        // Start is called before the first frame update
        /// <summary>
        ///  Start To load Menu Scene
        /// </summary>
        /// <returns></returns>
        IEnumerator Start()
        {
            scene = SceneManager.LoadSceneAsync("Menu");
            scene.allowSceneActivation = false;
            while (!scene.isDone)
            {
                if (scene.progress >= .9f)
                {
                    TechTween.ValueTo(gameObject, 0, 100, time).GetValueUpdate((value) =>
                    {
                        progrss = value;
                        if (value >= 100)
                        {
                            scene.allowSceneActivation = true;
                        }
                    });
                    break;
                }
                yield return null;
            }
        }
        private void OnEnable()
        {
            AnalyticsHandler.Instance.SceneLoad();
            AnalyticsHandler.Instance.PanelLoad(GetType().Name);
        }
        float progrss;
        private void Update()
        {
            m_LoadingText.text = (int)progrss + "%";
            m_LoadinImage.value = (progrss / 100);
        }
    }
}