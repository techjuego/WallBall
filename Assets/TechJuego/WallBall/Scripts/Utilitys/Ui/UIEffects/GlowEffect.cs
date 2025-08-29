using System.Collections.Generic;

namespace UnityEngine.UI
{
    /// <summary>
    /// This class is used to add a glow effect on UI elements
    /// </summary>
    [AddComponentMenu("UI/Effects/GlowEffect", 15)]
    public class GlowEffect : BaseMeshEffect
    {
        [SerializeField]
        private Color glowColor = new Color(1f, 1f, 1f, 0.5f);

        [SerializeField]
        private float glowSpread = 1f;

        [SerializeField]
        private int iterations = 5;

        [SerializeField]
        private bool m_UseGraphicAlpha = true;

        protected GlowEffect()
        { }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (graphic != null)
                graphic.SetVerticesDirty();
        }
#endif

        /// <summary>
        /// Property for glow color with automatic dirty marking
        /// </summary>
        public Color EffectColor
        {
            get { return glowColor; }
            set
            {
                glowColor = value;
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        /// <summary>
        /// Property for glow spread with automatic dirty marking
        /// </summary>
        public float GlowSpread
        {
            get { return glowSpread; }
            set
            {
                glowSpread = value;
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        /// <summary>
        /// Property for iterations with automatic dirty marking
        /// </summary>
        public int Iterations
        {
            get { return iterations; }
            set
            {
                iterations = value;
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        /// <summary>
        /// Property for using graphic alpha
        /// </summary>
        public bool UseGraphicAlpha
        {
            get { return m_UseGraphicAlpha; }
            set
            {
                m_UseGraphicAlpha = value;
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        /// <summary>
        /// Applies a glow effect by creating expanded versions of the graphic
        /// </summary>
        /// <param name="verts">List of vertices to modify</param>
        private void ApplyGlowEffect(List<UIVertex> verts)
        {
            int initialVertCount = verts.Count;
            List<UIVertex> vertsCopy = new List<UIVertex>(verts);
            verts.Clear();

            // Add glowing outlines with decreasing alpha
            for (int i = 0; i < iterations; i++)
            {
                float scaleFactor = 1f + (glowSpread * (i + 1) * 0.01f);
                Color iterationColor = glowColor;
                iterationColor.a = glowColor.a * (1 - ((float)i / iterations));

                // Add scaled vertices for each iteration
                for (int v = 0; v < initialVertCount; v++)
                {
                    UIVertex vertex = vertsCopy[v];
                    Vector3 position = vertex.position;

                    // Scale position outward from center
                    position = ScalePositionFromCenter(position, scaleFactor, vertsCopy);

                    vertex.position = position;
                    vertex.color = iterationColor;

                    verts.Add(vertex);
                }
            }

            // Add original vertices on top
            for (int i = 0; i < vertsCopy.Count; i++)
            {
                verts.Add(vertsCopy[i]);
            }
        }

        /// <summary>
        /// Scales a position from the center of all vertices
        /// </summary>
        private Vector3 ScalePositionFromCenter(Vector3 position, float scale, List<UIVertex> allVerts)
        {
            Vector3 center = GetCenter(allVerts);
            Vector3 direction = position - center;
            return center + (direction * scale);
        }

        /// <summary>
        /// Calculates the center of all vertices
        /// </summary>
        private Vector3 GetCenter(List<UIVertex> verts)
        {
            if (verts.Count == 0)
                return Vector3.zero;

            Vector3 min = verts[0].position;
            Vector3 max = verts[0].position;

            for (int i = 1; i < verts.Count; i++)
            {
                Vector3 pos = verts[i].position;
                min = Vector3.Min(min, pos);
                max = Vector3.Max(max, pos);
            }

            return (min + max) * 0.5f;
        }

        /// <summary>
        /// Modifies the mesh by applying the glow effect
        /// </summary>
        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
                return;

            List<UIVertex> output = new List<UIVertex>();
            vh.GetUIVertexStream(output);

            ApplyGlowEffect(output);

            vh.Clear();
            vh.AddUIVertexTriangleStream(output);
        }
    }
}