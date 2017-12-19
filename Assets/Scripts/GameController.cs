using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ez
{
    public class GameController : MonoBehaviour
    {
        public List<Element> Cubes;
        public List<Element> Circles;


        public Text ScoreText;
        private int Score = 0;

        public Text TimerText;


        private Timer Timer;
        
        
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

        
        /// <summary>
        /// увеличиваем счет на 1
        /// </summary>
        public void AddScore()
        {
            Score++;
            ScoreText.text = Score.ToString();
        }

        private void Awake()
        {
            Singleton();
            Element.TrueCollision += AddScore; // Подписка добавление счета на соприкосновение одинаковых элементов.
        }

        void Start()   
        {
             _generator.SetCurrentColor();
             
        }

        public void BeginTimer()
        {
            StartCoroutine(TimeOff(20));
        }

        /// <summary>
        /// Запустить таймер с заданным параметром. 
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        IEnumerator TimeOff(int startTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                startTime--;

                if (startTime < 0)
                {
                    TimerText.text = startTime.ToString();
                    Debug.LogError("Вы програли - время кончилось");
                }
            }
        }
       
    }
}