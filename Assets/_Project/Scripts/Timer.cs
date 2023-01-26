using System.Collections;
using UnityEngine;



public class Timer : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private float _timerDelay;
    [SerializeField] private bool _shouldRepeat;
    [SerializeField] private bool _shouldStartOnAwake;
    private float _startedTime;
    private float _elapsedTime;
    private  float _remainingTime;
    private bool _isValidTimer;
    private bool _isPausedTimer;
    private bool _isFinishedTimer;
    private IEnumerator _WaitCoroutime;
    private IEnumerator _updateCoroutime;
    private IEnumerator _delayCoroutime;


    public float TimerValue {get {return _time;} set { _time = value;}}
    public float StartedTime {get {return _startedTime;}}
    public float ElapsedTime {get {return _elapsedTime;}}
    public float RemainingTime {get {return _remainingTime;}}
    public bool IsValidTimer {get {return _isValidTimer;}}
    public bool IsFinishedTimer { get => _isFinishedTimer; }

    public float TimerDelay { get => _timerDelay; set { _timerDelay = value; } }

    public delegate void VoidDelegate();
    public delegate void FloatDelegate(float value);

    public event VoidDelegate onTimerPause;
    public event VoidDelegate onTimerStop;
    public event FloatDelegate onTimerUpdate;
    private void Start()
    {
        InvalidateTimer();

        if(_shouldStartOnAwake)
        {
            StartTimer();
        }
    }
    
    public Timer(float timerValue)
    {
        _time = timerValue;
    }

    public void PauseTimer()
    {
        _isPausedTimer = true;
        StopAllCoroutines();
    }

    public void ResumeTimer()
    {
        _isPausedTimer = false;
        _WaitCoroutime = Wait(_remainingTime);
        StartCoroutine(_WaitCoroutime);
    }

    public void StartTimer()
    {
        _isFinishedTimer = false;
        _isValidTimer = true;
        _isPausedTimer = false;
        _remainingTime = _time;
        _elapsedTime = 0;
        _startedTime = Time.time;
        _WaitCoroutime = Wait(_time);
        StartCoroutine(_WaitCoroutime);
        onTimerUpdate(_remainingTime);
    }

    public void StartTimer(float timerValue, float timerDelay)
    {
        _timerDelay = timerDelay;
        _time = timerValue;
        _isFinishedTimer = false;
        _isValidTimer = true;
        _isPausedTimer = false;
        _remainingTime = _time;
        _elapsedTime = 0;
        _startedTime = Time.time;
        
        StartCoroutine(WaitForTimerStart(_timerDelay));
    }

    /// <summary>
    /// if value is true - timer stopped manually, if it is false - timer stopped because time is over
    /// </summary>
    /// <param name="value"></param>
    public void StopTimer(bool value)
    {
        _isFinishedTimer = true;
        _isPausedTimer = false;

        StopAllCoroutines();
    
        if(value == false)
        {
            onTimerStop();
        }
       
        if(_shouldRepeat)
        {
            StartTimer();
        }        
    }

    public void InvalidateTimer()
    {     
        _isValidTimer = false;
        _isFinishedTimer = false;
        _isPausedTimer = false;
        
  
    }

    private IEnumerator Wait(float time)
    {
        _updateCoroutime = UpdateEachSecontdtimer(1f);
        StartCoroutine(_updateCoroutime);
        yield return new WaitForSeconds(time);
        StopTimer(false);
    }
    
    private IEnumerator UpdateEachSecontdtimer(float time)
    {
        yield return new WaitForSeconds(time);
        _remainingTime--;
        onTimerUpdate(_remainingTime);
        _updateCoroutime = UpdateEachSecontdtimer(time);
        StartCoroutine(_updateCoroutime);
    }
    
    private IEnumerator WaitForTimerStart(float time)
    {
        yield return new WaitForSeconds(time);
        _WaitCoroutime = Wait(_time);
        StartCoroutine(_WaitCoroutime);
        onTimerUpdate(_remainingTime);
    }

}
