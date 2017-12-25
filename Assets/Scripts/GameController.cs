using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ez
{
    public class GameController : MonoBehaviour
    {
        /// <summary>
        /// Кубики
        /// </summary>
        public List<Element> Cubes;

        /// <summary>
        /// Шарики
        /// </summary>
        public List<Element> Circles;

        /// <summary>
        /// создаем генератор
        /// </summary>
        public Generator Generator = new Generator();

        private int _bestScore;
        

        void CheckBestScore()
        {
          
            if (_score>_bestScore)
            {
                PlayerPrefs.SetInt("Score",Score);
            }
        }
        
        #region UI

        public Text ScoreText;
        private int _score = 0;

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                CheckBestScore();
                ScoreText.text = _score+"/"+_bestScore;
                
                if (Score % 10 == 0)
                {
                    AddBonusTime();
                }
            }
        }

        public Text TimerText;
        public int TimeGame;

        #endregion

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
        }

        /// <summary>
        /// увеличиваем счет на 10
        /// </summary>
        public void AddBonusTime()
        {
            TimeGame += 10;
        }

        private void Awake()
        {
            _bestScore = PlayerPrefs.GetInt("Score");
            Singleton();
            Element.TrueCollision += AddScore; // Подписка добавление счета на соприкосновение одинаковых элементов.
        }

        private void Start()
        {
            Generator.SetCurrentColor();
            TimeGame = Data.Instanse.TimeGame;
            BeginTimer();
        }

        /// <summary>
        /// Запустить таймер
        /// </summary>
        public void BeginTimer()
        {
            StartCoroutine(TimeOff());
        }

        /// <summary>
        /// Отдельный поток для таймера.
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        IEnumerator TimeOff()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                TimeGame--;

                TimerText.text = TimeGame.ToString();
                if (TimeGame < 0)
                {
                    Debug.LogError("Вы програли - время кончилось");
                }
            }
        }
        
    }
}