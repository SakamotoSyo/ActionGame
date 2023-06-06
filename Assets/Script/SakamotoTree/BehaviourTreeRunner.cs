using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
using System;

public class BehaviourTreeRunner : MonoBehaviour
{
    [SerializeField] private BehaviourTree _behaviour;
    [SerializeField] private Animator _conditionAnim;
    private Environment _env = new();
    private BehaviourTree _cloneBehaviour;
    private int _nodeCount;
    private void Start()
    {
        CloneBehaviorTree();
        EnvSetUp();
    }

    private void Update()
    {
        _cloneBehaviour.update(_env);
    }

    /// <summary>
    /// behaviorTreeに渡す情報をSetする
    /// </summary>
    public void EnvSetUp()
    {
        _env.mySelf = this.gameObject;
        _env.MySelfAnim = GetComponent<Animator>();
        _env.navMesh = GetComponent<NavMeshAgent>();
        _env.ConditionAnim = _conditionAnim;
        _env.target = ActorGenerator.PlayerObj;
    }

    /// <summary>
    /// behaviorTreeをクローンする
    /// メモ：2人の敵が同じScriptableObjを参照していた場合Dataが共有してしまうのでクローン処理を行っている
    /// </summary>
    private void CloneBehaviorTree()
    {
        _cloneBehaviour = ScriptableObject.CreateInstance<BehaviourTree>();
        _cloneBehaviour.RootNode = CloneNode(_behaviour.RootNode);
#if UNITY_EDITOR
        var path = "Assets/ParentNode";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        AssetDatabase.CreateAsset(_cloneBehaviour, Path.Combine(path, $"{gameObject.name + _cloneBehaviour.name}.asset"));
#endif
    }

    /// <summary>
    /// NodeをCloneする
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public Node CloneNode(Node node)
    {
        if (!node) return null;

        if (node is RootNode)
        {
            RootNode rootNode = node as RootNode;
            var cloneRootNode = ScriptableObject.CreateInstance<RootNode>();
            cloneRootNode.Child = CloneNode(rootNode.Child);
            CreateNodeAsset(cloneRootNode);
            node = cloneRootNode;
        }
        else if (node is ActionNode)
        {
            ActionNode actionNode = node as ActionNode;
            var cloneActionNode = Clone(actionNode);
            CreateNodeAsset(cloneActionNode);
            return cloneActionNode;
        }
        else if (node is ConditionNode)
        {
            ConditionNode conditionNode = node as ConditionNode;
            ConditionNode cloneCondiitonNode = Clone(conditionNode);
            List<Node> nodeChildren = new();
            for (int i = 0; i < conditionNode.NodeChildren.Count; i++)
            {
                nodeChildren.Add(CloneNode(conditionNode.NodeChildren[i]));
            }

            if (cloneCondiitonNode.NodeChildren != null)
            {
                cloneCondiitonNode.NodeChildren = nodeChildren;
            }
            CreateNodeAsset(cloneCondiitonNode);
            return cloneCondiitonNode;
        }
        else if (node is DecoratorNode)
        {
            DecoratorNode decoratorNode = node as DecoratorNode;
            DecoratorNode cloneDecoratorNode = Clone(decoratorNode);

            if (decoratorNode.Child)
            {
                cloneDecoratorNode.Child = CloneNode(decoratorNode.Child);
            }

            CreateNodeAsset(cloneDecoratorNode);
            return cloneDecoratorNode;
        }

        return node;
    }

    public T Clone<T>(T So) where T : ScriptableObject
    {
        string soName = So.name;
        So = Instantiate<T>(So);
        So.name = soName;
        return So;
    }

    private void CreateNodeAsset<T>(T node) where T : Node
    {
        var path = "Assets/ChildNode";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        var filePath = $"{node.name + _cloneBehaviour.name + _nodeCount}.asset";
        AssetDatabase.CreateAsset(node, Path.Combine(path, filePath));
        _cloneBehaviour.Nodes.Add(node);
        _nodeCount++;
    }
}
