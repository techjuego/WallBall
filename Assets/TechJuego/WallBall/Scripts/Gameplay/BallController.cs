using UnityEngine;
using TechJuego.Framework.HapticFeedback;
using TechJuego.Framework.Utils;
namespace TechJuego.Framework
{
    public class BallController : MonoBehaviour
    {
        private Rigidbody2D m_Rigidbody2D;
        public float force = 10;
        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            if (GameStateHandler.Instance.m_GameState == GameState.Gameplay)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    m_Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    if (GameManager.Instance.isLeft)
                    {
                        m_Rigidbody2D.velocity = new Vector2(-1, 1) * force;
                    }
                    else
                    {
                        m_Rigidbody2D.velocity = new Vector2(1, 1) * force;
                    }
                }
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.name.Contains("Left"))
            {
                GameManager.Instance.m_CurrentScore++;
                GameManager.Instance.isLeft = false;
                HapticCall.Instance.MediumHaptic();
            }
            if (collision.transform.name.Contains("Right"))
            {
                GameManager.Instance.m_CurrentScore++;
                GameManager.Instance.isLeft = true;
                HapticCall.Instance.MediumHaptic();
            }
            if (collision.transform.name.Contains("Obstacle"))
            {
                gameObject.SetActive(false);
                GameEvents.ShakeCameraSmall?.Invoke();
                GameManager.Instance.ShowBallDestroyEffect(transform.position);
                GameStateHandler.Instance.m_GameState = GameState.LevelFailed;
                HapticCall.Instance.HeavyHaptic();
            }
        }
    }
}