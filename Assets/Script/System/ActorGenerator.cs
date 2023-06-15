using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorGenerator : MonoBehaviour
{
    public static GameObject PlayerObj => _playerObj;

    public PlayerController PlayerCon { get; private set; }
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerInsTransform;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemyInsPos;
    private static GameObject _playerObj;

    private void Awake()
    {
        PlayerGeneration();
        EnemyGeneration();
    }

    void Start()
    {

    }


    void Update()
    {

    }

    public PlayerController PlayerGeneration()
    {
        _playerObj = Instantiate(_playerPrefab, _playerInsTransform.position, transform.rotation).transform.GetChild(0).gameObject;
        PlayerCon = _playerObj.GetComponent<PlayerController>();
        return PlayerCon;
    }

    public void EnemyGeneration()
    {
        for (int i = 0; i < 2; i++)
        {
            var enemyObj = Instantiate(_enemyPrefab, _enemyInsPos.position, transform.rotation);
            enemyObj.name = _enemyPrefab.name + i;
        }

    }
}
