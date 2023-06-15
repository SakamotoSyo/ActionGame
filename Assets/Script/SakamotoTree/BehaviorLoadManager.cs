using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public static class BehaviorLoadManager
{
    private static bool _isDebugBool = false;
    private static int _nodeCount = 0;
    private static BehaviourTree _cloneBehaviour;


    private static void Init()
    {
        _nodeCount = 0;
        _cloneBehaviour = null;
        _isDebugBool = false;
    }

    /// <summary>
    /// behaviorTreeをクローンする
    /// メモ：2人の敵が同じScriptableObjを参照していた場合Dataが共有してしまうのでクローン処理を行っている
    /// </summary>
    public static BehaviourTree CloneBehaviorTree(BehaviourTree behaviour, string objName, bool debugBool = false)
    {
        Init();
        _isDebugBool = debugBool;
        _cloneBehaviour = ScriptableObject.CreateInstance<BehaviourTree>();
        _cloneBehaviour.RootNode = CloneNode(behaviour.RootNode);
#if UNITY_EDITOR
        if (debugBool)
        {
            var path = "Assets/Debug/ParentNode";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            AssetDatabase.CreateAsset(_cloneBehaviour, Path.Combine(path, $"{objName + _cloneBehaviour.name}.asset"));
        }
#endif
        return _cloneBehaviour;
    }

    /// <summary>
    /// NodeをCloneする
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private static Node CloneNode(Node node)
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

    private static T Clone<T>(T So) where T : ScriptableObject
    {
        string soName = So.name;
        So = Object.Instantiate<T>(So);
        So.name = soName;
        return So;
    }

    private static void CreateNodeAsset<T>(T node) where T : Node
    {
        if (!_isDebugBool) return;

        var path = "Assets/Debug/ChildNode";
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
