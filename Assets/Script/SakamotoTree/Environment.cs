using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Environment
{
    public GameObject MySelf;
    public Rigidbody MySelfRb;
    public Animator MySelfAnim;
    public Animator ConditionAnim;
    public NavMeshAgent NavMesh;
    public GameObject Target;
    public ActorStatus ActorStatus;
}
