
using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Ez
{

    public class PlayerConroller : MonoBehaviour
    {


        public LayerMask CollisionDetected;
        private Vector3 startPosition; 
        public float speed = 0.1F;

        private int tapCount = 0;
        private float timeDoubleTap = 0.4f;
        private float newTime;
        
        private Transform Gragabble;
        private bool isGrabbing;
        
        
        private void Update()  // TODO: может надо сделать FixedUpdate() пока хер его
        {
#if UNITY_EDITOR
            UnityEditorController();
#endif

#if UNITY_ANDROID
            MobileController();
#endif
        }

        void UnityEditorController()
        {
            
            if (Input.GetMouseButtonDown(0))  // нажал на левую кнопку мыши
            {
                
                if (GetObject(Input.mousePosition))
                {
                    isGrabbing = true;
                }
            }
            if (Input.GetMouseButton(0))  // держишь левую кнопку мыши
            {
                if (isGrabbing)
                {
                    Moved(Input.mousePosition);                    
                }
            }
            if (Input.GetMouseButtonUp(0))  // отпустил левую кнопку мыши 
            {
                ResetFinger();
            }
            
            #region DoubleTap  
            // TODO: ВОт это гавно тоже надо сделать универсальным
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //TODO: надо ли постоянно выпускать лучь или нет. Хер его (потом зарефакторим)
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10, CollisionDetected) && Input.GetMouseButtonUp(0))
            {
                tapCount++;
                if (tapCount ==1)
                {
                    newTime = Time.time + timeDoubleTap;
                }else if (tapCount==2 && Time.time < newTime)
                {
                    GameController.Instanse._generator.getOneRandomColor(hit.collider.gameObject.GetComponent<Element>());
                }
            }
            if (Time.time > newTime)
            {
                tapCount = 0;
            }
            #endregion
            
            
        }

        void MobileController()
        {
             if (Input.touchCount<1)
                        return;

            #region Обработка нажатий пальцев
            switch (Input.GetTouch(0).phase)    //TODO: надо ли менять на конструкцию If() тоже не ясно, до рефакторинга так нагляднее.
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
                case TouchPhase.Canceled: //Система отменила отслеживание касания, например, когда (например) пользователь ставит устройство на свое лицо или одновременно происходит более пяти касаний. Это заключительная фаза прикосновения.
                    ResetFinger(); 
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            #endregion
            
            #region DoubleTap
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);  //TODO: надо ли постоянно выпускать лучь или нет. Хер его (потом зарефакторим)
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10, CollisionDetected) && Input.GetTouch(0).phase== TouchPhase.Ended)
            {
                tapCount++;
                if (tapCount ==1)
                {
                    newTime = Time.time + timeDoubleTap;
                }else if (tapCount==2 && Time.time < newTime)
                {
                        GameController.Instanse._generator.getOneRandomColor(hit.collider.gameObject.GetComponent<Element>());
                }
            }
            if (Time.time > newTime)
            {
                tapCount = 0;
            }
            #endregion
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
            Gragabble.position = Camera.main.ScreenToWorldPoint(position);
        }


    }
}


