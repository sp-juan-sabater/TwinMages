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

    private int _reboundCount;
    private Transform _mainCameraTransform;
    private LevelController _nextLevelController;
    
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
        _reboundCount = FsmVariables.GlobalVariables.FindFsmInt(_kReboundCountKey).Value;
        if (_reboundCount >= NeededReboundCounter && NextLevel && _nextLevelController)
        {
            Time.timeScale = 0;
            _mainCameraTransform.position = Vector3.MoveTowards(transform.position, _nextLevelController.GetCameraAnchorPosition(), CameraScrollSpeed);
        }
    }

    public Vector3 GetCameraAnchorPosition()
    {
        if (CameraAnchor)
        {
            return CameraAnchor.GetComponent<Transform>().position;
        }
        return new Vector3();
    }
    
}
