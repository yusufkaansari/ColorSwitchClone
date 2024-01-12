public struct Timer
{
    private float _currentTime;
    public float targetTime;

    public Timer(float targetTime)
    {
        _currentTime = 0f;
        this.targetTime = targetTime;
    }

    public void Tick(float deltaTime)
    {
        _currentTime += deltaTime;
    }

    public bool IsDone()
    {
        if (_currentTime >= targetTime)
        {
            _currentTime = 0;
            return true;
        }
        return false;
    }

    /* Use it:
        pritave Timer _timer;
        Awake() {
            _timer = new Timer(spawnInverval);
        }
        Update() {
            _timer.Tick(Time.deltaTime);
            if(_timer.IsDone())
                ...TODO
        }

     */
}