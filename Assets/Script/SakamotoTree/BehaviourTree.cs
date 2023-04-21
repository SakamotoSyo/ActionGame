using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[CreateAssetMenu()]
public class BehaviourTree : ScriptableObject
{
    public Node RootNode;
    private Node.State _treeState = Node.State.Running;
    public List<Node> Nodes = new List<Node>();

    public Node.State update(Environment env) 
    {
        return RootNode.update(env);
    }
//TODO:ここ後に修正
#if UNITY_EDITOR
    public Node CreateNode(Type type) 
    {
        Node node = CreateInstance(type) as Node;
        node.name = type.Name;
        node.Guid = GUID.Generate().ToString();
        Nodes.Add(node);

        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();
        return node;
    }

    public void DeleteNode(Node node) 
    {
        Nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }

    /// <summary>
    /// ノードつないだ時に参照を渡す処理
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="child"></param>
    public void AddChild(Node parent, Node child) 
    {
        ConditionNode conditionNode = parent as ConditionNode;
        if (conditionNode) 
        {
            conditionNode.NodeChildren.Add(child);
        }

        RootNode rootNode = parent as RootNode;
        if (rootNode) 
        {
            rootNode.Child = child;
        }
    }

    public void RemoveChild(Node parent, Node child) 
    {
        ConditionNode conditionNode = parent as ConditionNode;
        if (conditionNode)
        {
            conditionNode.NodeChildren.Remove(child);
        }


        RootNode rootNode = parent as RootNode;
        if (rootNode)
        {
            rootNode.Child = null;
        }
    }

    public List<Node> GetChildren(Node parent) 
    {
        //後々Nodeの種類を増やすことを考慮して作成
        List<Node> children = new();

        ConditionNode conditionNode = parent as ConditionNode;
        if (conditionNode)
        {
            return conditionNode.NodeChildren;
        }

        RootNode rootNode = parent as RootNode;
        if (rootNode && rootNode.Child != null)
        {
            children.Add(rootNode.Child);
        }

        return children;
    }
#endif
}
