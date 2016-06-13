using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    class SEvent
    {
        public voidEvent callBack;
        public float beginTime;
        public float delay;
        public int repeatNum;

        public SEvent(voidEvent ev, float delay, int repeatNum)
        {
            this.callBack = ev;
            this.delay = delay;
            this.repeatNum = repeatNum;
            beginTime = Time.realtimeSinceStartup;
        }
    }

    class UpdateEv
    {
        public floatEvent callBack;
        public float begin;
        public float total;
        public float counter;
        public float tweenTime;

        public UpdateEv(floatEvent ev, float begin, float end, float tweenTime)
        {
            this.callBack = ev;
            this.begin = begin;
            this.total = end - begin;
            this.counter = 0f;
            this.tweenTime = tweenTime;
        }
    }

    private static TimeManager _instance;

    public delegate void voidEvent();
    public delegate void floatEvent(float delta);

    private List<voidEvent> _allEvents;
    private List<SEvent> _allTimers;
    private UpdateEv _updateEv;

    void Awake()
    {
        _allEvents = new List<voidEvent>();
        _allTimers = new List<SEvent>();
    }

    void Update()
    {
        if (_allTimers.Count > 0)
        {
            for (int i = _allTimers.Count - 1; i >= 0; i--)
            {
                SEvent ev = _allTimers[i];
                if (ev.callBack == null)
                    _allTimers.Remove(ev);
                else if (Time.realtimeSinceStartup - ev.beginTime >= ev.delay)
                {
                    ev.repeatNum--;
                    if (ev.callBack != null)
                        ev.callBack();
                    if (ev.repeatNum <= 0)
                    {
                        _allTimers.Remove(ev);
                        ev = null;
                    }
                    else
                        ev.beginTime = Time.realtimeSinceStartup;
                }
            }
        }
        if (_updateEv != null)
        {
            _updateEv.counter += Time.deltaTime;
            if (_updateEv.callBack != null)
            {
                float p = _updateEv.counter / _updateEv.tweenTime;
                float delta = _updateEv.begin + _updateEv.total * p;
                _updateEv.callBack(delta);
            }
            if (_updateEv.counter >= _updateEv.tweenTime)
                _updateEv = null;
        }
    }

    //use corountine, affect by time.scale
    public void delayCall(voidEvent ev, float t)
    {
        StartCoroutine(callDelay(ev, t));
        _allEvents.Add(ev);
    }

    //use update, not affect by time scale
    public void setTimer(voidEvent ev, float delay, int repeatNum = 1, bool startNow = false)
    {
        if (startNow)
        {
            ev();
            repeatNum--;
        }
        if (repeatNum > 0)
        {
            SEvent sEv = new SEvent(ev, delay, repeatNum);
            _allTimers.Add(sEv);
        }
    }

    public void valueTo(float begin, float end, floatEvent ev, float t)
    {
        _updateEv = new UpdateEv(ev, begin, end, t);
    }

    public void stopAll()
    {
        StopAllCoroutines();
        if (_allEvents.Count > 0)
        {
            for (int i = 0; i < _allEvents.Count; i++)
                _allEvents[i] = null;
            _allEvents.Clear();
        }
        _allTimers.Clear();
    }

    private IEnumerator callDelay(voidEvent ev, float t)
    {
        yield return new WaitForSeconds(t);
        if (ev != null)
        {
            ev();
            _allEvents.Remove(ev);
            ev = null;
        }
    }

    public static TimeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = "Timer";
                _instance = obj.AddComponent<TimeManager>();
                Object.DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }
}
