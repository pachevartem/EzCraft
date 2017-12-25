using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ez
{
    /// <summary>
    /// Кругляшки, таймеры
    /// </summary>
    public class CircleTimer: MonoBehaviour
    {
        /// <summary>
        /// Картинка
        /// </summary>
        public Image progerssImage;
        
        public Element Element;
        
        /// <summary>
        /// Переменная для манипулирования переменной Fill Amount
        /// </summary>
        private float progress = 1;

        /// <summary>
        /// Получить число рандомное число
        /// </summary>
        /// <returns></returns>
        int RandomTime()
        {
            System.Random random = new System.Random(GetHashCode() + System.DateTime.Now.Millisecond);
            var a = random.Next(3,10);
            Debug.Log(a);
            return a;
        }

        
        /// <summary>
        /// Свойство для значения FillAmount
        /// </summary>
        public float Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                progerssImage.fillAmount = value;
            }
        }

        /// <summary>
        /// Отрабатывает в начале
        /// </summary>
        private void Start()
        {
           BeginCour();
        }
        
        /// <summary>
        /// Включить таймеры
        /// </summary>
        void BeginCour()
        {
            StopAllCoroutines();
            progress = 1;
            StartCoroutine(TimeOff(RandomTime()));
        }

        
        /// <summary>
        /// Отдельный поток, для таймера, в кончце работы перезагружается и меняет цвет.
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        IEnumerator TimeOff(float second)  
        {
            while (true)
            {
                yield return new WaitForSeconds(0.01f*second);
                Progress-= 0.01f;
                if (Progress<0)
                {
                    GameController.Instanse._generator.getOneRandomColorCorcle(Element,GameController.Instanse.Circles);
                    BeginCour();
                }
               
            }
        }

    }
}