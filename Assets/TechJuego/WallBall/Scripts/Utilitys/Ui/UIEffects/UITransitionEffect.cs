using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.UI
{
    /// <summary>
    /// This class is used to add transition animations to UI elements
    /// </summary>
    [AddComponentMenu("UI/Effects/UITransitionEffect", 19)]
    [RequireComponent(typeof(Graphic))]
    public class UITransitionEffect : MonoBehaviour
    {
        [Serializable]
        public enum TransitionType
        {
            Fade,
            Scale,
            Slide,
            Rotate
        }

        [Serializable]
        public enum EasingType
        {
            Linear,
            EaseIn,
            EaseOut,
            EaseInOut
        }

        [SerializeField]
        private TransitionType transitionType = TransitionType.Fade;

        [SerializeField]
        private EasingType easingType = EasingType.EaseInOut;

        [SerializeField]
        private float duration = 0.5f;

        [SerializeField]
        private bool playOnEnable = true;

        [SerializeField]
        private Vector2 slideDirection = Vector2.up;

        [SerializeField]
        private float slideDistance = 100f;

        [SerializeField]
        private Vector3 rotateAngles = new Vector3(0, 0, 90);

        [SerializeField]
        private Vector3 scaleStart = new Vector3(0.5f, 0.5f, 1);

        [SerializeField]
        private Vector3 scaleEnd = Vector3.one;

        private Graphic targetGraphic;
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Vector3 originalPosition;
        private Vector3 originalScale;
        private Vector3 originalRotation;
        private float originalAlpha;
        private Coroutine currentTransition;

        private void Awake()
        {
            targetGraphic = GetComponent<Graphic>();
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();

            // Add canvas group if needed for fade transitions
            if (canvasGroup == null && transitionType == TransitionType.Fade)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            // Store original state
            originalPosition = rectTransform.localPosition;
            originalScale = rectTransform.localScale;
            originalRotation = rectTransform.localEulerAngles;
            originalAlpha = canvasGroup != null ? canvasGroup.alpha : 1f;
        }

        private void OnEnable()
        {
            if (playOnEnable)
            {
                PlayTransitionIn();
            }
        }

        private void OnDisable()
        {
            if (currentTransition != null)
            {
                StopCoroutine(currentTransition);
                currentTransition = null;
            }

            // Reset to original state
            if (rectTransform != null)
            {
                rectTransform.localPosition = originalPosition;
                rectTransform.localScale = originalScale;
                rectTransform.localEulerAngles = originalRotation;
            }

            if (canvasGroup != null)
            {
                canvasGroup.alpha = originalAlpha;
            }
        }

        /// <summary>
        /// Play the transition in (appearing)
        /// </summary>
        public void PlayTransitionIn()
        {
            if (currentTransition != null)
            {
                StopCoroutine(currentTransition);
            }

            // Setup initial state based on transition type
            switch (transitionType)
            {
                case TransitionType.Fade:
                    if (canvasGroup != null)
                    {
                        canvasGroup.alpha = 0;
                        currentTransition = StartCoroutine(FadeTransition(0, originalAlpha));
                    }
                    break;

                case TransitionType.Scale:
                    rectTransform.localScale = scaleStart;
                    currentTransition = StartCoroutine(ScaleTransition(scaleStart, scaleEnd));
                    break;

                case TransitionType.Slide:
                    Vector3 startPos = originalPosition + new Vector3(
                        slideDirection.x * slideDistance,
                        slideDirection.y * slideDistance,
                        0);
                    rectTransform.localPosition = startPos;
                    currentTransition = StartCoroutine(SlideTransition(startPos, originalPosition));
                    break;

                case TransitionType.Rotate:
                    Vector3 startRotation = originalRotation + rotateAngles;
                    rectTransform.localEulerAngles = startRotation;
                    currentTransition = StartCoroutine(RotateTransition(startRotation, originalRotation));
                    break;
            }
        }

        /// <summary>
        /// Play the transition out (disappearing)
        /// </summary>
        public void PlayTransitionOut()
        {
            if (currentTransition != null)
            {
                StopCoroutine(currentTransition);
            }

            switch (transitionType)
            {
                case TransitionType.Fade:
                    if (canvasGroup != null)
                    {
                        currentTransition = StartCoroutine(FadeTransition(canvasGroup.alpha, 0));
                    }
                    break;

                case TransitionType.Scale:
                    currentTransition = StartCoroutine(ScaleTransition(rectTransform.localScale, scaleStart));
                    break;

                case TransitionType.Slide:
                    Vector3 endPos = originalPosition + new Vector3(
                        slideDirection.x * slideDistance,
                        slideDirection.y * slideDistance,
                        0);
                    currentTransition = StartCoroutine(SlideTransition(rectTransform.localPosition, endPos));
                    break;

                case TransitionType.Rotate:
                    Vector3 endRotation = originalRotation + rotateAngles;
                    currentTransition = StartCoroutine(RotateTransition(rectTransform.localEulerAngles, endRotation));
                    break;
            }
        }

        private IEnumerator FadeTransition(float startAlpha, float endAlpha)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsed / duration);
                float easedTime = ApplyEasing(normalizedTime);

                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, easedTime);
                yield return null;
            }

            canvasGroup.alpha = endAlpha;
            currentTransition = null;
        }

        private IEnumerator ScaleTransition(Vector3 startScale, Vector3 endScale)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsed / duration);
                float easedTime = ApplyEasing(normalizedTime);

                rectTransform.localScale = Vector3.Lerp(startScale, endScale, easedTime);
                yield return null;
            }

            rectTransform.localScale = endScale;
            currentTransition = null;
        }

        private IEnumerator SlideTransition(Vector3 startPosition, Vector3 endPosition)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsed / duration);
                float easedTime = ApplyEasing(normalizedTime);

                rectTransform.localPosition = Vector3.Lerp(startPosition, endPosition, easedTime);
                yield return null;
            }

            rectTransform.localPosition = endPosition;
            currentTransition = null;
        }

        private IEnumerator RotateTransition(Vector3 startRotation, Vector3 endRotation)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsed / duration);
                float easedTime = ApplyEasing(normalizedTime);

                rectTransform.localEulerAngles = Vector3.Lerp(startRotation, endRotation, easedTime);
                yield return null;
            }

            rectTransform.localEulerAngles = endRotation;
            currentTransition = null;
        }

        /// <summary>
        /// Apply easing function to the normalized time
        /// </summary>
        private float ApplyEasing(float t)
        {
            switch (easingType)
            {
                case EasingType.EaseIn:
                    return t * t;
                case EasingType.EaseOut:
                    return 1 - (1 - t) * (1 - t);
                case EasingType.EaseInOut:
                    return t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
                default: // Linear
                    return t;
            }
        }
    }
}