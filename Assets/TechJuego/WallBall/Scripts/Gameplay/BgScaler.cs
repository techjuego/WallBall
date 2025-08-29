using UnityEngine;
namespace TechJuego.Framework
{
    public class BgScaler : MonoBehaviour
    {
        public Camera cam;
        public float widthRatio = 1f;
        public float heightRatio = 1f;
        void SetScale()
        {
            if (cam == null)
                cam = Camera.main;
            if (cam.orthographic)
            {
                float height = cam.orthographicSize * 2f;
                float width = height * cam.aspect;
                transform.localScale = new Vector3(width * widthRatio, height * heightRatio, transform.localScale.z);
            }
        }
        private void OnValidate()
        {
            SetScale();
        }
        private void OnEnable()
        {
            SetScale();
        }
    }
}