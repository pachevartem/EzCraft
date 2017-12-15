using System.Collections.Generic;
using UnityEngine;

namespace Ez
{
    public class GameController: MonoBehaviour
    {

        public List<Element> Cubes;
        
        Generator _generator = new Generator();


        void Start()
        {
            _generator.Fill();
            _generator.Gen();
            SetColor();
        }

        void SetColor()
        {
            for (int i = 0; i < 3; i++)
            {
                Cubes[i].MyMaterials.color = _generator._generateColor[i];
            }
        }


    }
}