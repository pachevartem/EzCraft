using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Linq;


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
