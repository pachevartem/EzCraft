using System.Collections.Generic;
using UnityEngine;

namespace Ez
{
    public class GameController: MonoBehaviour
    {

        public List<Element> Cubes;
        public List<Element> Circles;
     public   Generator _generator = new Generator();
        
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
        void Start()
        {
            _generator.Fill(); 
            _generator.Gen();
            SetColor(Cubes);
            _generator.Fill();     
            _generator.Gen();
            SetColor(Circles);
        }

        void SetColor(List<Element> elements)
        {
            for (int i = 0; i < 3; i++)
            {
                elements[i].MyMaterials.color = _generator._generateColor[i];
            }
        }


    }
}