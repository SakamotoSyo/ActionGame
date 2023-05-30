using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourTreeRunner : MonoBehaviour
{
    [SerializeField] private BehaviourTree _behaviour;
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _conditionAnim;
    private Environment _env= new();

    private void Start()
    {
        _env.mySelf = this.gameObject;
        _env.MySelfAnim = GetComponent<Animator>();
        _env.navMesh = GetComponent<NavMeshAgent>();
        _env.ConditionAnim = _conditionAnim;
        _env.target = _player;
    }

    private void Update()
    {
        _behaviour.update(_env);
    }

}
