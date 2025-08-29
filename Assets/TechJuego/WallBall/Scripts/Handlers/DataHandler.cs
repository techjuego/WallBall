using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TechJuego.Framework
{
    public class DataHandler : Singleton<DataHandler>
    {
        protected DataHandler()
        {
        }
        public void Load()
        {
        }

        public int GetHighScore()
        {
            return PlayerPrefs.GetInt("HIGH");
        }
        public  void SetHighScore(int score)
        {
            if(GetHighScore() < score)
            {
                PlayerPrefs.SetInt("HIGH", score);
            }
        }
    }
}