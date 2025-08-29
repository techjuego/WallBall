using System;
using UnityEngine;
namespace TechJuego.Framework
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }
        private static GameManager _instance;
        public GameManager()
        {
            _instance = this;
        }
        [SerializeField] private GameObject m_BallDestroy;
        private void Awake()
        {
            CurrentScore = 0;
        }
        public void ShowBallDestroyEffect(Vector3 position)
        {
            Instantiate(m_BallDestroy,position,Quaternion.identity);
        }
        public void ShowWinEffect(Vector3 position)
        {
        }
        private int CurrentScore;
        public int m_CurrentScore
        {
            get { return CurrentScore; }
            set
            {
                CurrentScore = value;
                GameEvents.OnStarCollect?.Invoke();
            }
        }
        public bool isLeft;
    }
}