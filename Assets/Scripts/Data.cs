using System.Collections.Generic;
using UnityEngine;


namespace Ez
{
    /// <summary>
    /// Будет хранить в себе исходные данные. 
    /// </summary>
    public class Data : MonoBehaviour
    {
        public List<Color> Colors;
        public ScriptableData SO;

        public int TimeGame;


        #region Singleton

        public static Data Instanse;

        void Singleton()
        {
            if (Instanse != null && Instanse != this)
            {
                Destroy(gameObject);
                return;
            }

            Instanse = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        /// <summary>
        /// Установка объектов
        /// </summary>
        public void SetupGame()
        {
            for (int i = 0; i < SO.GameColors.Count; i++)
            {
                Colors.Add(SO.GameColors[i]);
            }
            TimeGame = SO.TimeGame;
        }

        
        private void Awake()
        {
            Singleton();
            SetupGame();
        }
    }
}