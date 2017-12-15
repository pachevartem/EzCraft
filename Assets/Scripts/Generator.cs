using System.Collections.Generic;
using UnityEngine;

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
            
            for (int i = 0; i < Data.Instanse.CountColor; i++)
            {
                _currentColor.Add(Data.Instanse.Colors[i]);
            }
        }

       public void Gen()
        {
            for (int i = 0; i < 3; i++)
            {
                getRandomColor();
            }
        }

        /// <summary>
        /// Получаем цвет из заданого списка
        /// </summary>
        /// <returns></returns>
        Color getRandomColor()
        {
            int indexColor = Random.Range(0, _currentColor.Count);
            var vColor = _currentColor[indexColor];
            _generateColor.Add(_currentColor[indexColor]);
            _currentColor.RemoveAt(indexColor);
            return new Color();
        }


    }
}