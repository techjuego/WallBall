using UnityEngine;
using System.Collections;
using System;
namespace TechJuego.Framework
{
    public class CameraShake : MonoBehaviour
    {
        public bool canShake = false;
        public float shakeAmount = 0.7f;
        public float decreaseFactor = 1.0f;

        Vector3 originalPos;

        void OnEnable()
        {
            originalPos = transform.localPosition;
            GameEvents.ShakeCameraBig += CamerashakeBig;
            GameEvents.ShakeCameraSmall += CamerashakeSmall;
        }

        void OnDisable()
        {
            GameEvents.ShakeCameraBig -= CamerashakeBig;
            GameEvents.ShakeCameraSmall -= CamerashakeSmall;
        }

        void LateUpdate()
        {
            if (canShake)
            {
                // Add random shake in both X and Y
                float offsetX = UnityEngine.Random.Range(-1f, 1f) * shakeAmount;
                float offsetY = UnityEngine.Random.Range(-1f, 1f) * shakeAmount;

                transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0f);
            }
            else
            {
                // Smoothly return to original position
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPos, Time.deltaTime * 20);
            }
        }

        void CamerashakeBig()
        {
            canShake = true;
            StartCoroutine(DelayCall(decreaseFactor * 2, () =>
            {
                canShake = false;
            }));
        }

        void CamerashakeSmall()
        {
            canShake = true;
            StartCoroutine(DelayCall(decreaseFactor, () =>
            {
                canShake = false;
            }));
        }

        IEnumerator DelayCall(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }
    }
}