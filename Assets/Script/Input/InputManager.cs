using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;


public class InputManager : SingletonBehaviour<InputManager>
{
    public Vector3 MoveDir => _moveDir;

    [Tooltip("InputSystem�Ő�������Scrpt")]
    PlayerAction _inputManager;
    [Tooltip("�ړ��������")]
    Vector3 _moveDir;
    [Tooltip("���͒���")]
    Dictionary<InputType, Action> _onEnterInputDic = new Dictionary<InputType, Action>();
    [Tooltip("���͉���")]
    Dictionary<InputType, Action> _onExitInputDic = new Dictionary<InputType, Action>();
    [Tooltip("���͒�")]
    Dictionary<InputType, bool> _isStayInputDic = new Dictionary<InputType, bool>();

    bool _isInstanced = false;

    protected override void OnAwake()
    {
        Initialize();
    }

    /// <summary>
    /// ����������
    /// </summary>
    void Initialize()
    {
        _inputManager = new PlayerAction();
        _inputManager.Enable();
        InirializeInput();
        _inputManager.Player.Move.performed += context => _moveDir = context.ReadValue<Vector3>();
        _inputManager.Player.Move.canceled += context => _moveDir = Vector3.zero;
        _inputManager.Player.Attack.started += context => ExecuteInput(InputType.Attack, InputMode.Enter);
        _inputManager.Player.Attack.canceled += context => ExecuteInput(InputType.Attack, InputMode.Exit);
        _inputManager.Player.MenuOpen.started += context => ExecuteInput(InputType.Menu, InputMode.Enter);
        _inputManager.Player.MenuOpen.canceled += context => ExecuteInput(InputType.Menu, InputMode.Exit);
        _isInstanced = true;
    }

    private void Update()
    {
      
    }

    /// <summary>
    /// ���͏����̏��������s��
    /// </summary>
    void InirializeInput()
    {
        if (_isInstanced)
        {
            for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
            {
                _onEnterInputDic[(InputType)i] = null;
                _onExitInputDic[(InputType)i] = null;
                _isStayInputDic[(InputType)i] = false;
            }
            return;
        }
        for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
        {
            _onEnterInputDic.Add((InputType)i, null);
            _onExitInputDic.Add((InputType)i, null);
            _isStayInputDic.Add((InputType)i, false);
        }
    }

    /// <summary>
    /// ���͊J�n���͉��������Ƃ��ɌĂ΂��֐�
    /// </summary>
    /// <param name="input"></param>
    void ExecuteInput(InputType input, InputMode type)
    {
       
        switch (type)
        {
            case InputMode.Enter:
                //���͊J�n���������s����
                _onEnterInputDic[input]?.Invoke();
                break;
            case InputMode.Exit:
                // ���͉������������s����
                _onExitInputDic[input]?.Invoke();
                SetStayInput(input , false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ����InputType�����͒����ǂ����t���O��Ԃ�
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool GetStayInput(InputType type)
    {
        return _isStayInputDic[type];
    }

    /// <summary>
    ///����̓��͂ŌĂяo��Action��o�^����
    /// </summary>
    public void SetEnterInput(InputType type, Action action) 
    {
        _onEnterInputDic[type] += action;
    }

    public void SetStayInput(InputType type, bool isBool) 
    {
        Debug.Log($"{type}��{isBool}�ɂȂ��Ă��܂�");
        _isStayInputDic[type] = isBool;
    }

    /// <summary>
    ///����̓��͏I��������ɌĂяo��Action��o�^����
    /// </summary>
    public void SetExitInput(InputType type, Action action) 
    {
        _onExitInputDic[type] += action;
    }

    /// <summary>
    /// ����̓��͂ŌĂяo�����o�^����Action���폜����
    /// </summary>
    public void LiftEnterInput(InputType type, Action action) 
    {
        _onEnterInputDic[type] -= action;
    }

    /// <summary>
    ///����̓��͏I��������ɌĂяo�����o�^����Action���폜����
    /// </summary>
    public void LiftExitInput(InputType type, Action action)
    {
        _onExitInputDic[type] -= action;
    }


    /// <summary>
    /// ���͂̃^�C�~���O
    /// </summary>
    public enum InputMode
    {
        /// <summary>���͎�</summary>
        Enter,
        /// <summary>���͏I����</summary>
        Exit,
    }

    /// <summary>
    /// ���͂̎��
    /// </summary>
    public enum InputType
    {
        /// <summary>�L�����Z���̏���</summary>
        Cancel,
        /// <summary>�U��</summary>
        Attack,
        /// <summary>���j���[���J��</summary>
        Menu,
    }
}

