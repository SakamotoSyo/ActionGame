using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;


public class InputManager : SingletonBehaviour<InputManager>
{
    public Vector3 MoveDir => _moveDir;

    [Tooltip("InputSystemで生成したScrpt")]
    PlayerAction _inputManager;
    [Tooltip("移動する向き")]
    Vector3 _moveDir;
    [Tooltip("入力直後")]
    Dictionary<InputType, Action> _onEnterInputDic = new Dictionary<InputType, Action>();
    [Tooltip("入力解除")]
    Dictionary<InputType, Action> _onExitInputDic = new Dictionary<InputType, Action>();
    [Tooltip("入力中")]
    Dictionary<InputType, bool> _isStayInputDic = new Dictionary<InputType, bool>();

    bool _isInstanced = false;

    protected override void OnAwake()
    {
        Initialize();
    }

    /// <summary>
    /// 初期化処理
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
    /// 入力処理の初期化を行う
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
    /// 入力開始入力解除したときに呼ばれる関数
    /// </summary>
    /// <param name="input"></param>
    void ExecuteInput(InputType input, InputMode type)
    {
       
        switch (type)
        {
            case InputMode.Enter:
                //入力開始処理を実行する
                _onEnterInputDic[input]?.Invoke();
                break;
            case InputMode.Exit:
                // 入力解除処理を実行する
                _onExitInputDic[input]?.Invoke();
                SetStayInput(input , false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// そのInputTypeが入力中かどうかフラグを返す
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool GetStayInput(InputType type)
    {
        return _isStayInputDic[type];
    }

    /// <summary>
    ///特定の入力で呼び出すActionを登録する
    /// </summary>
    public void SetEnterInput(InputType type, Action action) 
    {
        _onEnterInputDic[type] += action;
    }

    public void SetStayInput(InputType type, bool isBool) 
    {
        Debug.Log($"{type}が{isBool}になっています");
        _isStayInputDic[type] = isBool;
    }

    /// <summary>
    ///特定の入力終わった時に呼び出すActionを登録する
    /// </summary>
    public void SetExitInput(InputType type, Action action) 
    {
        _onExitInputDic[type] += action;
    }

    /// <summary>
    /// 特定の入力で呼び出される登録したActionを削除する
    /// </summary>
    public void LiftEnterInput(InputType type, Action action) 
    {
        _onEnterInputDic[type] -= action;
    }

    /// <summary>
    ///特定の入力終わった時に呼び出される登録したActionを削除する
    /// </summary>
    public void LiftExitInput(InputType type, Action action)
    {
        _onExitInputDic[type] -= action;
    }


    /// <summary>
    /// 入力のタイミング
    /// </summary>
    public enum InputMode
    {
        /// <summary>入力時</summary>
        Enter,
        /// <summary>入力終了時</summary>
        Exit,
    }

    /// <summary>
    /// 入力の種類
    /// </summary>
    public enum InputType
    {
        /// <summary>キャンセルの処理</summary>
        Cancel,
        /// <summary>攻撃</summary>
        Attack,
        /// <summary>メニューを開く</summary>
        Menu,
    }
}

