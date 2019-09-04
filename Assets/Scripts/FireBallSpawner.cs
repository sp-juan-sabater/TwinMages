using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FireBallSpawner : MonoBehaviour
{
    [SerializeField] public float spawnTimeOffset;
    [SerializeField] public GameObject playerA;
    [SerializeField] public GameObject playerB;
    [SerializeField] public GameObject FireBall;
    
    private bool _lastSpawnerPlayerA = true;
    private Transform _playerATransform;
    private Transform _playerBTransform;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(playerA, "playerA can't be null");
        Assert.IsNotNull(playerB, "playerB can't be null");
        Assert.IsNotNull(FireBall, "FireBall can't be null");

        _playerATransform = playerA.GetComponent<Transform>();
        _playerBTransform = playerB.GetComponent<Transform>();

        SetNeedSpawnFireball();
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnFireBall()
    {
        yield return new WaitForSeconds(spawnTimeOffset);
        Vector3 originPosition = _lastSpawnerPlayerA ? _playerATransform.position : _playerBTransform.position;
        Instantiate(FireBall, originPosition, Quaternion.identity);
    }

    public void SetNeedSpawnFireball()
    {
        StartCoroutine(SpawnFireBall());
    }
}
