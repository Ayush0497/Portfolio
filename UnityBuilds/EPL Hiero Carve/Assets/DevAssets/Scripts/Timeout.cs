using TouchScript;
using Unity.VisualScripting;
using UnityEngine;

public class Timeout : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    private const float _END_TIME_DURATION = 30f;
    private float _endTimeUntilTimeout = _END_TIME_DURATION;

    public float GameTimeUntilTimeout = 60f;
    private bool _countDownRunning = false;
    [SerializeField]
    private bool _endScreen;
    //[SerializeField]
    //private LoadShape _loadShape;

    [SerializeField]
    private GameManager _thisManager, _otherManager1, _otherManager2;
    public bool TrackTime;
    [SerializeField]
    private Timeout _otherTimeout1, _otherTimeout2;

    private void OnEnable()
    {
        if (!_endScreen)
        {
            if (_thisManager.CoopEnabled && !_otherManager1.CoopEnabled && !_otherManager2.CoopEnabled)
            {
                TrackTime = true;
            }
            else if (_thisManager.CoopEnabled && _otherManager1.CoopEnabled && !_otherManager2.CoopEnabled
            ||
            _thisManager.CoopEnabled && _otherManager1.CoopEnabled && _otherManager2.CoopEnabled)
            {

                if (!TrackTime && !_otherTimeout1.TrackTime && !_otherTimeout2.TrackTime)
                {
                    TrackTime = true;
                }
            }
            TouchManager.Instance.PointersPressed += ResetTimer;
            TouchManager.Instance.PointersUpdated += ResetTimer;
            TouchManager.Instance.PointersReleased += ResetTimer;
        }

        _countDownRunning = true;
    }

    private void OnDisable()
    {
        if (_endScreen)
        {
            _gameManager.Reset();
            _endTimeUntilTimeout = _END_TIME_DURATION;
        }
        else
        {
            //TouchManager.Instance.PointersPressed -= ResetTimer;
            //TouchManager.Instance.PointersUpdated -= ResetTimer;
            //TouchManager.Instance.PointersReleased -= ResetTimer;

            GameTimeUntilTimeout = 60f;
            TrackTime = false;
        }
    }

    private void Update()
    {
        if (_endScreen)
        {
            ///<summary>
            /// Triggers if this is the end screen
            /// If the countdown reaches zero then take the player back to the title screen.
            /// </summary>
            if (_countDownRunning)
            {
                if (_endTimeUntilTimeout > 0)
                {
                    _endTimeUntilTimeout -= Time.deltaTime;
                }
                else
                {
                    _endTimeUntilTimeout = 0;
                    _countDownRunning = false;
                    //_loadShape.ResetGame();
                    //if (GameObject.FindGameObjectWithTag("Coop"))
                    //{
                    //    GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().Reset();
                    //}
                    GetComponent<ChangeScreen>().ChangeTheScreen();
                }
            }
        }
        else
        {
            ///<summary>
            /// If the countdown reaches zero then take the player back to the title screen.
            /// </summary>
            if (_countDownRunning)
            {
                if (_thisManager.CoopEnabled && !TrackTime)
                {
                    if (GameObject.FindGameObjectWithTag("Coop"))
                    {
                        float time = GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().TimeOut;
                        GameTimeUntilTimeout = time;
                    }
                }
                else
                {
                    if (TrackTime && GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().TimeOut == 60)
                    {
                        float time = GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().TimeOut;
                        GameTimeUntilTimeout = time;
                    }
                    if (GameTimeUntilTimeout > 0)
                    {
                        GameTimeUntilTimeout -= Time.deltaTime;
                    }
                    if (TrackTime)
                    {
                        if (GameObject.FindGameObjectWithTag("Coop"))
                        {
                            GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().TimeOut = GameTimeUntilTimeout;
                        }
                    }
                }

                if (GameTimeUntilTimeout <= 0)
                {
                    GameTimeUntilTimeout = 0;
                    _countDownRunning = false;
                    //_loadShape.ResetGame();
                    GetComponent<ChangeScreen>().ChangeTheScreen();
                }
            }
        }
    }

    private void ResetTimer(object sender, PointerEventArgs e)
    {
        Vector2 pointerPosition = e.Pointers[e.Pointers.Count - 1].Position;
        bool isPointerInside = RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), pointerPosition, Camera.main);

        if (isPointerInside)
        {
            GameTimeUntilTimeout = 60f;
            if (_thisManager.CoopEnabled)
            {
                if (GameObject.FindGameObjectWithTag("Coop"))
                {
                    GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().TimeOut = GameTimeUntilTimeout;
                }
            }
        }
    }
}
