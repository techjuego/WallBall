using System.Collections.Generic;

namespace UnityEngine.UI
{
    /// <summary>
    /// This class is used to add an outline to UI elements
    /// </summary>
    [AddComponentMenu("UI/Effects/OutlineEffect", 16)]
    public class OutlineEffect : BaseMeshEffect
    {
        [SerializeField]
        private Color outlineColor = new Color(0f, 0f, 0f, 1f);

        [SerializeField]
        private Vector2 outlineDistance = new Vector2(1f, 1f);

        [SerializeField]
        private bool m_UseGraphicAlpha = true;

        protected OutlineEffect()
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
        /// Property for outline color with automatic dirty marking
        /// </summary>
        public Color EffectColor
        {
            get { return outlineColor; }
            set
            {
                outlineColor = value;
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        /// <summary>
        /// Property for outline distance with automatic dirty marking
        /// </summary>
        public Vector2 EffectDistance
        {
            get { return outlineDistance; }
            set
            {
                outlineDistance = value;
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
        /// Applies the outline effect by creating multiple offset copies of the vertices
        /// </summary>
        void ApplyOutlineEffect(List<UIVertex> verts)
        {
            int initialVertCount = verts.Count;
            List<UIVertex> vertsCopy = new List<UIVertex>(verts);
            verts.Clear();

            // Create outline by offsetting vertices in 8 directions
            Vector2[] directions = new Vector2[]
            {
                new Vector2(1, 1),   // top-right
                new Vector2(1, 0),   // right
                new Vector2(1, -1),  // bottom-right
                new Vector2(0, -1),  // bottom
                new Vector2(-1, -1), // bottom-left
                new Vector2(-1, 0),  // left
                new Vector2(-1, 1),  // top-left
                new Vector2(0, 1)    // top
            };

            for (int d = 0; d < directions.Length; d++)
            {
                Vector2 dir = directions[d];
                for (int i = 0; i < initialVertCount; i++)
                {
                    UIVertex vt = vertsCopy[i];
                    vt.position.x += dir.x * outlineDistance.x;
                    vt.position.y += dir.y * outlineDistance.y;

                    Color vertColor = outlineColor;
                    if (m_UseGraphicAlpha)
                        vertColor.a *= vertsCopy[i].color.a / 255f;

                    vt.color = vertColor;
                    verts.Add(vt);
                }
            }

            // Add original vertices on top
            for (int i = 0; i < vertsCopy.Count; i++)
            {
                verts.Add(vertsCopy[i]);
            }
        }

        /// <summary>
        /// Modifies the mesh by applying the outline effect
        /// </summary>
        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
                return;

            List<UIVertex> output = new List<UIVertex>();
            vh.GetUIVertexStream(output);

            ApplyOutlineEffect(output);

            vh.Clear();
            vh.AddUIVertexTriangleStream(output);
        }
    }
}