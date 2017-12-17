using System.Collections.Generic;
using UnityEngine;

namespace Ez
{
    public class GameController : MonoBehaviour
    {
        public List<Element> Cubes;
        public List<Element> Circles;
        
      
        
        
        public Generator _generator = new Generator();

        public static GameController Instanse;

        #region Singleton

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


        private void Awake()
        {
            Singleton();
        }

        void Start()   //TODO: такое гавно тут, но сейчас это работает
        {
         _generator.SetCurrentColor();
        }

       
    }
}