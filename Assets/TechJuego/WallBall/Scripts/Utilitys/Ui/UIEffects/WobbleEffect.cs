using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    /// <summary>
    /// This class is used to add a wobble effect to UI elements
    /// </summary>
    [AddComponentMenu("UI/Effects/WobbleEffect", 20)]
    public class WobbleEffect : BaseMeshEffect
    {
        [SerializeField]
        private float wobbleAmplitude = 5f;

        [SerializeField]
        private float wobbleFrequency = 2f;

        [SerializeField]
        private float wobbleSpeed = 1f;

        [SerializeField]
        private bool animateInEditor = false;

        private float timeOffset;

        protected override void Awake()
        {
            timeOffset = Random.Range(0f, 100f);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            wobbleAmplitude = Mathf.Max(0f, wobbleAmplitude);
            wobbleFrequency = Mathf.Max(0.1f, wobbleFrequency);
            wobbleSpeed = Mathf.Max(0.1f, wobbleSpeed);

            if (graphic != null && animateInEditor)
                graphic.SetVerticesDirty();
        }
#endif

        void Update()
        {
            if (graphic != null)
                graphic.SetVerticesDirty();
        }

        /// <summary>
        /// Property for wobble amplitude with automatic dirty marking
        /// </summary>
        public float WobbleAmplitude
        {
            get { return wobbleAmplitude; }
            set
            {
                wobbleAmplitude = Mathf.Max(0f, value);
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        /// <summary>
        /// Property for wobble frequency with automatic dirty marking
        /// </summary>
        public float WobbleFrequency
        {
            get { return wobbleFrequency; }
            set
            {
                wobbleFrequency = Mathf.Max(0.1f, value);
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        /// <summary>
        /// Property for wobble speed with automatic dirty marking
        /// </summary>
        public float WobbleSpeed
        {
            get { return wobbleSpeed; }
            set
            {
                wobbleSpeed = Mathf.Max(0.1f, value);
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        /// <summary>
        /// Applies the wobble effect by displacing vertices in a wave pattern
        /// </summary>
        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive() || vh.currentVertCount == 0)
                return;

            List<UIVertex> vertices = new List<UIVertex>();
            vh.GetUIVertexStream(vertices);

            WobbleVertices(vertices);

            vh.Clear();
            vh.AddUIVertexTriangleStream(vertices);
        }

        /// <summary>
        /// Applies a sinusoidal displacement to vertices
        /// </summary>
        private void WobbleVertices(List<UIVertex> verts)
        {
            float timeValue = Time.time * wobbleSpeed + timeOffset;
            Rect rect = graphic.rectTransform.rect;
            Vector2 center = rect.center;

            // Find bounds
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;

            for (int i = 0; i < verts.Count; i++)
            {
                Vector3 pos = verts[i].position;
                minX = Mathf.Min(minX, pos.x);
                minY = Mathf.Min(minY, pos.y);
                maxX = Mathf.Max(maxX, pos.x);
                maxY = Mathf.Max(maxY, pos.y);
            }

            float width = maxX - minX;
            float height = maxY - minY;

            // Apply wobble to each vertex
            for (int i = 0; i < verts.Count; i++)
            {
                UIVertex vertex = verts[i];
                Vector3 pos = vertex.position;

                // Normalize position for wobble calculation
                float normalizedX = (pos.x - minX) / width;
                float normalizedY = (pos.y - minY) / height;

                // Calculate displacement
                float displacementX = Mathf.Sin(normalizedY * wobbleFrequency + timeValue) * wobbleAmplitude;
                float displacementY = Mathf.Sin(normalizedX * wobbleFrequency + timeValue) * wobbleAmplitude;

                // Apply displacement
                pos.x += displacementX;
                pos.y += displacementY;

                vertex.position = pos;
                verts[i] = vertex;
            }
        }
    }
}