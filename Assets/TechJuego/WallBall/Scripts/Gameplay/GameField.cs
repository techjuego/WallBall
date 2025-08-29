using UnityEngine;
using System.Collections.Generic;
using TechJuego.Framework.Utils;
namespace TechJuego.Framework
{
    public class GameField : MonoBehaviour
    {
        public List<GameObject> m_LeftObstacle = new List<GameObject>();
        public List<GameObject> m_RightObstacle = new List<GameObject>();
        private void OnEnable()
        {
            DisableAllObstacles();
            GameEvents.OnStarCollect += GameEvents_OnStarCollect;
            GameStateHandler.Instance.m_GameState = GameState.Gameplay;
        }
        private void OnDisable()
        {
            GameEvents.OnStarCollect -= GameEvents_OnStarCollect;
        }
        private void GameEvents_OnStarCollect()
        {
            TechTween.DelayCall(gameObject, 0.2f, () =>
            {
                if (GameManager.Instance.isLeft)
                {
                    UpdateLeftObstacle();
                }
                else
                {
                    UpdateRightObstacles();
                }
            });
        }
        public int startObstacleScore = 2;   // First obstacle spawns at 2 points
        void DisableAllObstacles()
        {
            foreach (var obs in m_LeftObstacle) obs.SetActive(false);
            foreach (var obs in m_RightObstacle) obs.SetActive(false);
        }
        void UpdateRightObstacles()
        {
            // No obstacles until reaching start score
            if (GameManager.Instance.m_CurrentScore < startObstacleScore)
            {
                DisableAllObstacles();
                return;
            }
            // Number of active obstacles grows with score
            int activeCount = Random.Range(0, m_LeftObstacle.Count-1); 
            // Right side
            EnableRandomObstacles(m_RightObstacle, activeCount);
        }

        void UpdateLeftObstacle()
        {
            // No obstacles until reaching start score
            if (GameManager.Instance.m_CurrentScore < startObstacleScore)
            {
                DisableAllObstacles();
                return;
            }
            // Number of active obstacles grows with score
            int activeCount = Mathf.Min(GameManager.Instance.m_CurrentScore / 2, m_LeftObstacle.Count);
            // Left side
            EnableRandomObstacles(m_LeftObstacle, activeCount);
        }
        void EnableRandomObstacles(List<GameObject> obstacles, int count)
        {
            // Disable all first
            foreach (var obs in obstacles) obs.SetActive(false);

            // Always leave 2 free gaps
            int gapIndex1 = Random.Range(0, obstacles.Count);
            int gapIndex2;
            do
            {
                gapIndex2 = Random.Range(0, obstacles.Count);
            } while (gapIndex2 == gapIndex1);

            // Shuffle obstacle indices
            List<int> indices = new List<int>();
            for (int i = 0; i < obstacles.Count; i++) indices.Add(i);
            Shuffle(indices);

            // Activate obstacles except the chosen gaps
            int enabled = 0;
            foreach (int i in indices)
            {
                if (i == gapIndex1 || i == gapIndex2) continue; // keep two gaps
                obstacles[i].SetActive(true);
                enabled++;
                if (enabled >= count) break;
            }
        }

        // Simple shuffle helper
        void Shuffle(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int rand = Random.Range(i, list.Count);
                int temp = list[i];
                list[i] = list[rand];
                list[rand] = temp;
            }
        }
    }
}