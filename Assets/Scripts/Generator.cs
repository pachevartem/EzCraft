using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ez
{
    /// <summary>
    /// Содержит в себе генераторы цветов
    /// </summary>
    public class Generator
    {
        /// <summary>
        /// Заполняем массив из заданного
        /// </summary>
        public void SetCurrentColor()   //TODO: возможно перенести метод в GameController из за частой связи с Singloton
        {
            SetupElement(GameController.Instanse.Cubes,3);
            SetupElement(GameController.Instanse.Circles,3);
        }
    
        /// <summary>
        /// Установить элементы в массив
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="counElement"></param>
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

        /// <summary>
        /// Сгенерировать один цвет
        /// </summary>
        /// <param name="element"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void getOneRandomColor(Element element, List<Element> a, List<Element> b)  //TODO: может быть перенести этот метод в GameController, слишком много обращений к Singleton
        {
            if (ExistColor(element.Color, a)) //GameController.Instanse.Circles
            {
                return;
            }
            Color vColor = new Color();
            do
            {
                int indexColor = Random.Range(0, 3);
                var gColor = GenerateSetColor(3, Data.Instanse.Colors);
                vColor = gColor[indexColor];
            } while (ExistColor(vColor,b )); //GameController.Instanse.Cubes
            element.Color = vColor;
        }
        
        /// <summary>
        /// Сгенерировать один цвет для колец
        /// </summary>
        /// <param name="element"></param>
        /// <param name="elements"></param>
        public void getOneRandomColorCorcle(Element element, List<Element> elements)
        {
            Color vColor = new Color();
            do
            {
                int indexColor = Random.Range(0, 3);
                var gColor = GenerateSetColor(3, Data.Instanse.Colors);
                vColor = gColor[indexColor];
            } while (ExistColor(vColor,elements)); //GameController.Instanse.Cubes
            element.Color = vColor;
        }

        /// <summary>
        /// Узнать есть ли такой цвет в листе
        /// </summary>
        /// <param name="color"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        bool ExistColor(Color color, List<Element> elements) 
        {
            for (int i = 0; i < 3; i++)
            {
                if (elements[i].Color == color )
                {
                    return true;
                }
            }
            return false;
        }
    }
}