//using TouchScript;
//using UnityEngine;

//public class dummyTimeout : MonoBehaviour
//{
//    [SerializeField]
//    private GameManager _gameManager;
//    private float _endTimeUntilTimeout = 30;
//    [SerializeField]
//    private float _gameTimeUntilTimeout = 60;
//    private bool _countDownRunning = false;
//    [SerializeField]
//    private bool _endScreen;

//    private void OnEnable()
//    {
//        if (!_endScreen)
//        {
//            TouchManager.Instance.PointersPressed += ResetTimer;
//            TouchManager.Instance.PointersUpdated += ResetTimer;
//            TouchManager.Instance.PointersReleased += ResetTimer;
//        }

//        _countDownRunning = true;
//    }

//    private void OnDisable()
//    {
//        if (_endScreen)
//        {
//            _gameManager.Reset();
//            _endTimeUntilTimeout = 30;
//        }
//        else
//        {
//            TouchManager.Instance.PointersPressed -= ResetTimer;
//            TouchManager.Instance.PointersUpdated -= ResetTimer;
//            TouchManager.Instance.PointersReleased -= ResetTimer;

//            _gameTimeUntilTimeout = 60;
//        }
//    }

//    private void Update()
//    {
//        if (_endScreen)
//        {
//            ///<summary>
//            /// Triggers if this is the end screen
//            /// If the countdown reaches zero then take the player back to the title screen.
//            /// </summary>
//            if (_countDownRunning)
//            {
//                if (_endTimeUntilTimeout > 0)
//                {
//                    _endTimeUntilTimeout -= Time.deltaTime;
//                }
//                else
//                {
//                    _endTimeUntilTimeout = 0;
//                    _countDownRunning = false;
//                    GetComponent<ChangeScreen>().ChangeTheScreen();
//                }
//            }
//        }
//        else
//        {
//            ///<summary>
//            /// If the countdown reaches zero then take the player back to the title screen.
//            /// </summary>
//            if (_countDownRunning)
//            {
//                if (_gameTimeUntilTimeout > 0)
//                {
//                    _gameTimeUntilTimeout -= Time.deltaTime;
//                }
//                else
//                {
//                    _gameTimeUntilTimeout = 0;
//                    _countDownRunning = false;
//                    GetComponent<ChangeScreen>().ChangeTheScreen();
//                }
//            }
//        }
//    }

//    private void ResetTimer(object sender, PointerEventArgs e)
//    {
//        Vector2 pointerPosition = e.Pointers[e.Pointers.Count - 1].Position;
//        bool isPointerInside = RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), pointerPosition, Camera.main);

//        if (isPointerInside)
//        {
//            _gameTimeUntilTimeout = 60;
//        }
//    }
//}
