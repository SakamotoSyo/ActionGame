using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    [SerializeField] private BehaviourTree _behaviour;
    [SerializeField] private GameObject _player;
    private Environment _env= new();

    private void Start()
    {
        _env.mySelf = _player;
    }

    private void Update()
    {
        _behaviour.update(_env);
    }

}
