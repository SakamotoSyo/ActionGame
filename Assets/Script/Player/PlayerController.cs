using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput = new();
    [SerializeField] private PlayerMove _playerMove = new();
    //[SerializeField] private

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        
    }

    private void Move()
    {
        _playerMove.FixedUpdate();
    }

}
