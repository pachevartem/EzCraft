using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ez
{
    public class Generator
    {
      
        
        /// <summary>
        /// Заполняем массив из заданного
        /// </summary>
        public void SetCurrentColor()
        {
            SetupElement(GameController.Instanse.Cubes,3);
            SetupElement(GameController.Instanse.Circles,3);
        }

        
        void SetupElement(List<Element> elements, int counElement)
        {
            var generateColor = GenerateSetColor(counElement, Data.Instanse.Colors);
            for (int i = 0; i < counElement; i++)
            {
                elements[i].Color = generateColor[i];
            }
        }



        /// <summary>
        /// возвращает сгенерированный лист цветов
        /// </summary>
        /// <param name="count"> укажите колличесвто цветов, не превышающих заданное количество</param>
        /// <returns> что тут?</returns>
        List<Color> GenerateSetColor(int count, List<Color> fromData)
        {
            
            if (count>fromData.Count)
            {
                Debug.LogError("Выходим за предела заданог массива");
                Debug.Break();
            }

            var bufferList = new List<Color>();
            for (int i = 0; i < fromData.Count; i++)
            {
                bufferList.Add(Data.Instanse.Colors[i]);
            }
            
            var colors = new List<Color>();
            for (int i = 0; i < count; i++)
            {
                colors.Add(getRandomColor(bufferList));
            }
            
            return colors;
        }


        /// <summary>
        /// Получаем цвет из заданого списка
        /// </summary>
        /// <returns></returns>
        Color getRandomColor(List<Color> buffer)
        {
            int indexColor = Random.Range(0, buffer.Count);
            var vColor = buffer[indexColor];
            buffer.RemoveAt(indexColor);
            return vColor;
        }


        public void getOneRandomColor(Element element)
        {
            if (ExistColor(element.Color, GameController.Instanse.Circles))
            {
                return;
            }
            Color vColor = new Color();
            do
            {
                int indexColor = Random.Range(0, 3);
                Debug.Log("Сгенерировал число - " + indexColor);
                var gColor = GenerateSetColor(3, Data.Instanse.Colors);
                Debug.Log("Длинна - " + gColor.Count);
                vColor = gColor[indexColor];
            } while (ExistColor(vColor, GameController.Instanse.Cubes));

            element.Color = vColor;
        }

        bool ExistColor(Color color, List<Element> elements)
        {
            for (int i = 0; i < 3; i++)
            {
                if (elements[i].Color == color )
                {
                    Debug.Log("Сверяю");
                    return true;
                }
            }
            return false;
        }
    }
}