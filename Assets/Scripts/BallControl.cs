using UnityEngine;

namespace ColorSwitch
{
    public class BallControl : MonoBehaviour
    {
        [SerializeField] private float _ziplamaKuvveti = 300f;
        [SerializeField] private float _ziplamaFrequency = 1f;
        [SerializeField] private Color topunRengi;
        [SerializeField] private Color turkuaz;
        [SerializeField] private Color sari;
        [SerializeField] private Color mor;
        [SerializeField] private Color kirmizi;
        [SerializeField] private Renkler mevcutRenk;

        private SpriteRenderer _spriteRenderer;

        //Ziplama
        private Timer _timer;

        private Rigidbody2D _rb;
        private bool _canZiplama = false;

        private LevelManager _levelManager;
        private CircleControl _cemberKontrolu;
        private GameManager _gameManager;
        private ScorePoint _scorePoint;
        private void Awake()
        {
           
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
            _timer = new Timer(_ziplamaFrequency);
        }
        private void Start()
        {
            if (Waypoint.TryGetWaypoint(out LevelManager levelManager)) { _levelManager = levelManager; }
            if (Waypoint.TryGetWaypoint(out GameManager gameManager)) { _gameManager = gameManager; }
            if (Waypoint.TryGetWaypoint(out CircleControl cemberKontrolu)) { _cemberKontrolu = cemberKontrolu; }
            if (Waypoint.TryGetWaypoint(out ScorePoint scorePoint)) { _scorePoint = scorePoint; }
            RastgeleRenkBelirle();
        }
        private void TouchControl()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    _canZiplama = true;
                }
            }
        }
        private void MouseControl()
        {
            if (Input.GetMouseButtonDown(0) && _timer.IsDone())
            {
                _canZiplama = true;
            }
        }
        private void Update()
        {
            _timer.Tick(Time.deltaTime);
#if UNITY_EDITOR
            MouseControl();
#elif UNITY_WEBGL
    MouseControl();
#elif UNITY_ANDROID
    TouchControl();
#endif
        }
        private void FixedUpdate()
        {
            if (_canZiplama)
            {
                Ziplama();
                _canZiplama = false;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Vector3 desiredPosition = Vector3.up * 13;
            if (collision.TryGetComponent(out CircleColor cemberRengi))
            {
                if (cemberRengi.renk != mevcutRenk)
                {
                    _gameManager.PlayWrong();
                    _gameManager.GameOver();
                }
            }
            else if(collision.TryGetComponent(out ShiftObject shiftObject))
            {
                collision.transform.position += desiredPosition;
                _cemberKontrolu.transform.position += desiredPosition;
                _scorePoint.gameObject.SetActive(true);
                _cemberKontrolu.solaDon = !_cemberKontrolu.solaDon;
                _cemberKontrolu.RandomDonmeHizi();
                _ziplamaKuvveti += 5f;

            }
            else if(collision.TryGetComponent(out ScorePoint scorePoint))
            {
                _gameManager.IncreaseScore(50);
                _scorePoint.gameObject.SetActive(false);
            }
            else if(collision.TryGetComponent(out DeathZone deathZone))
            {
                _gameManager.GameOver();
            }
            else if (collision.TryGetComponent(out ColorChanger colorChanger))
            {
                _gameManager.PlayColorChange();
                collision.transform.position += desiredPosition;
                RastgeleRenkBelirle();
            }
        }
        private void Ziplama()
        {
            _gameManager.PlayClick();
            _rb.linearVelocity = Vector2.up * _ziplamaKuvveti * Time.fixedDeltaTime;
        }
        private void RastgeleRenkBelirle()
        {
            int random = Random.Range(0, 4);

            switch (random)
            {
                case 0:
                    mevcutRenk = Renkler.Turkuaz;
                    topunRengi = turkuaz;
                    break;

                case 1:
                    mevcutRenk = Renkler.Sari;
                    topunRengi = sari;
                    break;

                case 2:
                    mevcutRenk = Renkler.Kirmizi;
                    topunRengi = kirmizi;
                    break;

                case 3:
                    mevcutRenk = Renkler.Mor;
                    topunRengi = mor;
                    break;
            }
            _spriteRenderer.color = topunRengi;
        }
    }
}