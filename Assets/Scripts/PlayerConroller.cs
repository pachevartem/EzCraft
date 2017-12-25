using System;
using UnityEngine;

namespace Ez
{
    /// <summary>
    /// Класс описывающий работу игрока
    /// </summary>
    public class PlayerConroller : MonoBehaviour
    {
        /// <summary>
        /// Физический слой
        /// </summary>
        public LayerMask CollisionDetected;

        /// <summary>
        /// Стартовая позиция
        /// </summary>
        private Vector3 startPosition;

        /// <summary>
        /// Количество нажатий, переменная используется для отслеживания двойного нажатия
        /// </summary>
        private int tapCount = 0;

        /// <summary>
        /// Задержка, дельта между двойным нажатием
        /// </summary>
        private float timeDoubleTap = 0.4f;

        /// <summary>
        /// Переменная засикающая время, для отслеживания переменных
        /// </summary>
        private float newTime;

        /// <summary>
        /// Переменна для отслеживания перемещения мыши
        /// </summary>
        private Vector3 oldVector3 = Vector3.zero;

        /// <summary>
        /// Перемещаемый объект
        /// </summary>
        private Transform Gragabble;

        /// <summary>
        /// Показывает статус объекта, перемещается или нет
        /// </summary>
        private bool isGrabbing;

        private GameController _gc;
        private Generator _gen;

        /// <summary>
        /// Отрабатывается перед 1 кадрос
        /// </summary>
        void Awake()
        {
            Element.FailCollision += ResetFinger;
            Element.TrueCollision += ResetFinger;
        }

        private void Start()
        {
            SetStaticCariable(); //TODO: Разобраться со всеми инициализациями
        }

        /// <summary>
        /// хешируем ссылки на синглтон
        /// </summary>
        void SetStaticCariable()
        {
            _gc = GameController.Instanse;
            _gen = _gc.Generator;
        }

        /// <summary>
        /// Отрабатывает каждые кадр отрисовки
        /// </summary>
        private void Update()
        {
#if UNITY_EDITOR
            UnityEditorController(); // Будет работать если ты в юнити
#endif

#if UNITY_ANDROID
            MobileController(); // Будет работать если ты сбилдил под Ведра
#endif
        }

        /// <summary>
        /// Отарабатывает в конце отрисовки кадра
        /// </summary>
        private void LateUpdate()
        {
            SaveOldPisition();
        }

        /// <summary>
        /// метод нужен только для контроля движения мыши
        /// </summary>
        void SaveOldPisition()
        {
            oldVector3 = Input.mousePosition;
        }

        /// <summary>
        /// Проверяем двигается ли мышка
        /// </summary>
        /// <returns></returns>
        bool isMouseMoved()
        {
            return Math.Abs(Vector3.Distance(oldVector3, Input.mousePosition)) > 0;
        }


        /// <summary>
        /// Метод взаимодействия с объектами с помощью мыши
        /// </summary>
        void UnityEditorController()
        {
            if (Input.GetMouseButtonDown(0)) // нажал на левую кнопку мыши
            {
                if (GetObject(Input.mousePosition))
                {
                    isGrabbing = true;
                }
            }
            if (Input.GetMouseButton(0)) // держишь левую кнопку мыши
            {
                if (isGrabbing && isMouseMoved())
                {
                    Moved(Input.mousePosition);
                }
            }
            if (Input.GetMouseButtonUp(0)) // отпустил левую кнопку мыши 
            {
                ResetFinger();
            }

            DoubleTap();
        }

        /// <summary>
        /// Двойной тап
        /// </summary>
        void DoubleTap()
        {
            Ray ray;
            bool RaySolver;
            RaycastHit hit;
#if UNITY_EDITOR
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaySolver = Physics.Raycast(ray, out hit, 10, CollisionDetected) && Input.GetMouseButtonUp(0);
#endif

#if PLATFORM_ANDROID

            if (Input.touchCount == 1)
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaySolver = Physics.Raycast(ray, out hit, 10, CollisionDetected) &&
                            Input.GetTouch(0).phase == TouchPhase.Ended;
            }
#endif


            if (RaySolver)
            {
                tapCount++;
                if (tapCount == 1)
                {
                    newTime = Time.time + timeDoubleTap;
                }
                else if (tapCount == 2 && Time.time < newTime)
                {
                    _gen.GetOneRandomColor(hit.collider.gameObject.GetComponent<Element>(), _gc.Circles, _gc.Cubes);
                }
            }

            if (Time.time > newTime)
            {
                tapCount = 0;
            }
        }

        /// <summary>
        /// Сенсорные экраны
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        void MobileController()
        {
            if (Input.touchCount < 1)
                return;

            #region Обработка нажатий пальцев

            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began: // Палец только что прикоснулся к экрану.
                    if (GetObject(Input.GetTouch(0).position))
                    {
                        isGrabbing = true;
                    }
                    break;
                case TouchPhase.Moved: //Палец передвинулся по экрану.
                    if (isGrabbing)
                    {
                        Moved(Input.GetTouch(0).position);
                    }
                    break;
                case TouchPhase.Stationary: // Палец прикоснулся к экрану, но с последнего кадра не двигался.
                    if (isGrabbing)
                    {
                        Moved(Input.GetTouch(0).position);
                    }
                    break;
                case TouchPhase.Ended: // Палец только что оторван от экрана. Это последняя фаза нажатий.

                    ResetFinger();

                    break;
                case TouchPhase.Canceled
                : //Система отменила отслеживание касания, например, когда (например) пользователь ставит устройство на свое лицо или одновременно происходит более пяти касаний. Это заключительная фаза прикосновения.
                    ResetFinger();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            #endregion

            DoubleTap();
        }

        /// <summary>
        /// Возвращает есть ли объект под пальцем в момент нажатия
        /// </summary>
        /// <returns></returns>
        bool GetObject(Vector3 position)
        {
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10, CollisionDetected))
            {
                Gragabble = hit.collider.transform;
                startPosition = Gragabble.position;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Возвращает объект на место
        /// </summary>
        void ResetFinger()
        {
            if (!Gragabble)
            {
                return;
            }
            Gragabble.position = startPosition;
            Gragabble = null;
            isGrabbing = false;
        }

        /// <summary>
        /// Перемещение объекта в позицию мыши или нажатия пальца
        /// </summary>
        void Moved(Vector3 position)
        {
            var pos = Camera.main.ScreenToWorldPoint(position);
            Gragabble.position = new Vector3(pos.x, pos.y, 0);
        }
    }
}