using System.Collections;
using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelController : MonoBehaviour
{
    [SerializeField] public int NeededReboundCounter;
    [SerializeField] public GameObject NextLevel;
    [SerializeField] public GameObject CameraAnchor;
    [SerializeField] public float CameraScrollSpeed;
    
    //Constants
    private string _kReboundCountKey = "ReboundCount";

    private FsmInt _reboundCount;
    private Transform _mainCameraTransform;
    private LevelController _nextLevelController;
    public delegate void LevelComplete();
    public static event LevelComplete OnTravelling;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCameraTransform = Camera.main.GetComponent<Transform>();
        if (NextLevel)
        {
            Assert.IsNotNull(CameraAnchor, "CameraAnchor can't be null");
            _nextLevelController = NextLevel.GetComponent<LevelController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _reboundCount = FsmVariables.GlobalVariables.FindFsmInt(_kReboundCountKey);
        if (_reboundCount.Value >= NeededReboundCounter && NextLevel && _nextLevelController)
        {
            StartCoroutine(MovePieceTowards());
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
    }

    public Vector3 GetCameraAnchorPosition()
    {
        if (CameraAnchor)
        {
            return CameraAnchor.transform.position;
        }
        return Vector3.zero;
    }
    
}
