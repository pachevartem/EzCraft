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

        
        
        #region UI
        
        public Text ScoreText;
        private int Score = 0;
        
        public Text TimerText;
        private Timer Timer;
        
        #endregion
        
        public Generator _generator = new Generator();

        #region Singleton
        public static GameController Instanse;
       
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
        private void Start()
        {
             _generator.SetCurrentColor();
             BeginTimer();
        }

        /// <summary>
        /// Запустить таймер
        /// </summary>
        public void BeginTimer()
        {
            StartCoroutine(TimeOff(Data.Instanse.TimeGame));
        }
        
        /// <summary>
        /// Отдельный поток для таймера.
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        IEnumerator TimeOff(int startTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                startTime--;

                TimerText.text = startTime.ToString();
                if (startTime < 0)
                {
                    Debug.LogError("Вы програли - время кончилось");
                }    
            }
        }
           
    }
}