using System.Collections;
using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    [SerializeField] public int NeededReboundCounter;
    [SerializeField] public GameObject NextLevel;
    [SerializeField] public GameObject CameraAnchor;
    [SerializeField] public float CameraScrollSpeed;
    [SerializeField] public GameObject PlayerA;
    [SerializeField] public GameObject PlayerB;
    [SerializeField] public GameObject PlayerAAnchor;
    [SerializeField] public GameObject PlayerBAnchor;
    [SerializeField] public GameObject GameManager;
    
    public UnityEvent OnTravellingActivated;
    public UnityEvent OnTravellingDesactivated;
    
    //Constants
    private string _kReboundCountKey = "ReboundCount";
    private string _kFireBallTagKey = "Fireball";
    private string _kFirstFireBallKey = "FirstFireball";

    private FsmInt _reboundCount;
    private Transform _mainCameraTransform;
    private Transform _playerATransform;
    private Transform _playerBTransform;
    private LevelController _nextLevelController;
    private PlayMakerFSM _gameManagerfsm;

    // Start is called before the first frame update
    void Start()
    {
        _mainCameraTransform = Camera.main.GetComponent<Transform>();
        if (NextLevel)
        {
            Assert.IsNotNull(CameraAnchor, "CameraAnchor can't be null");
            Assert.IsNotNull(PlayerA, "PlayerA can't be null");
            Assert.IsNotNull(PlayerB, "PlayerB can't be null");
            Assert.IsNotNull(GameManager, "GameManager can't be null");
            _nextLevelController = NextLevel.GetComponent<LevelController>();
            _playerATransform = PlayerA.GetComponent<Transform>();
            _playerBTransform = PlayerB.GetComponent<Transform>();
            _gameManagerfsm = GameManager.GetComponent<PlayMakerFSM>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _reboundCount = FsmVariables.GlobalVariables.FindFsmInt(_kReboundCountKey);
        if (_reboundCount.Value >= NeededReboundCounter && NextLevel && _nextLevelController)
        {
            StartCoroutine(MovePieceTowards());
            OnTravellingActivated.Invoke();
            GameObject fireball = GameObject.FindGameObjectWithTag(_kFireBallTagKey);
            if (fireball)
            {
                Destroy(fireball);
            }
            _reboundCount.Value = 0;
        }
    }
    
    IEnumerator MovePieceTowards()
    {
        while (_mainCameraTransform.position != _nextLevelController.GetCameraAnchorPosition())
        {
            _mainCameraTransform.position = Vector3.MoveTowards(_mainCameraTransform.position, _nextLevelController.GetCameraAnchorPosition(), CameraScrollSpeed*Time.deltaTime);
            yield return null;
        }

        _playerATransform.position = _nextLevelController.GetPlayerASpawnPosition();
        _playerBTransform.position = _nextLevelController.GetPlayerBSpawnPosition();
        OnTravellingDesactivated.Invoke();
        
        _gameManagerfsm.SendEvent(_kFirstFireBallKey);
                        //.Event("CreateFireball");
    }

    public Vector3 GetCameraAnchorPosition()
    {
        if (CameraAnchor)
        {
            return CameraAnchor.transform.position;
        }
        return Vector3.zero;
    }

    public Vector3 GetPlayerASpawnPosition()
    {
        if (PlayerAAnchor)
        {
            return PlayerAAnchor.transform.position;
        }
        return Vector3.zero;
    }
    
    public Vector3 GetPlayerBSpawnPosition()
    {
        if (PlayerBAnchor)
        {
            return PlayerBAnchor.transform.position;
        }
        return Vector3.zero;
    }
}
