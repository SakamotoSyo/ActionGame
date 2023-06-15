using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : CharacterBase
{
    [SerializeField] int _moveSpeed;
    Evaluator _evl = new Evaluator();
    Rigidbody _rb;
    float _h;
    float _v;

    public Evaluator Evaluator { get => _evl; }
    private void Awake()
    {
        //_evl.SetPlayer(this);
        _rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        var dirForward = Vector3.forward * _v + Vector3.right * _h;
        dirForward = Camera.main.transform.TransformDirection(dirForward);
        dirForward.y = 0;
        _rb.velocity = dirForward.normalized * _moveSpeed + _rb.velocity.y * Vector3.up; ;
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterBase enemy = other.GetComponent<CharacterBase>();
        if (enemy)
        {
            _evl.SetEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterBase enemy = other.GetComponent<CharacterBase>();
        if (enemy)
        {
            _evl.RemoveEnemy(enemy);
        }
    }
}
