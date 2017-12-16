using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ez
{
    public class Generator
    {
       
        private List<Color> _currentColor = new List<Color>();
        public List<Color> _generateColor = new List<Color>();

        
        /// <summary>
        /// Заполняем массив для генерации
        /// </summary>
       public void Fill()
        {
            _currentColor.Clear();
            _generateColor.Clear();
            for (int i = 0; i < Data.Instanse.CountColor; i++)
            {
                _currentColor.Add(Data.Instanse.Colors[i]);
            }
        }

       public void Gen()
        {
            for (int i = 0; i < 3; i++)
            {
              _generateColor.Add(getRandomColor());
            }
//            Debug.Log("End Geberate");
        }

        /// <summary>
        /// Получаем цвет из заданого списка
        /// </summary>
        /// <returns></returns>
        Color getRandomColor()
        {
            int indexColor = Random.Range(0, _currentColor.Count);
            var vColor = _currentColor[indexColor];
            _currentColor.RemoveAt(indexColor);
            return vColor;
        }

 

       public void getOneRandomColor(Element element)
        {
            Fill();
            Color  vColor = new Color();
            do
            {
                int indexColor = Random.Range(0, _currentColor.Count);
                vColor = _currentColor[indexColor];
            } while (_generateColor.Contains(vColor));

            element.MyMaterials.color = vColor;

        }


    }
}