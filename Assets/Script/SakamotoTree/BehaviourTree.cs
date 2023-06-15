using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;


[Serializable][CreateAssetMenu()]
public class BehaviourTree : ScriptableObject
{
    public Node RootNode;
    private Node.State _treeState = Node.State.Running;
    [SerializeField] public List<Node> Nodes = new List<Node>();
    private Stack<Node> _nodeStack = new Stack<Node>();

    public Node.State update(Environment env) 
    {
        return RootNode.update(env);
    }
//TODO:Ç±Ç±å„Ç…èCê≥
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
        _nodeStack.Push(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }

    /// <summary>
    /// ÉmÅ[ÉhÇ¬Ç»Ç¢ÇæéûÇ…éQè∆ÇìnÇ∑èàóù
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

        DecoratorNode decoratorNode = parent as DecoratorNode;
        if (decoratorNode) 
        {
       
            decoratorNode.Child = child;
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


        DecoratorNode decoratorNode = parent as DecoratorNode;
        if (decoratorNode)
        {

            decoratorNode.Child = null;
        }
    }

    public List<Node> GetChildren(Node parent) 
    {
        //å„ÅXNodeÇÃéÌóﬁÇëùÇ‚Ç∑Ç±Ç∆Ççló∂ÇµÇƒçÏê¨
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

        DecoratorNode decoratorNode = parent as DecoratorNode;
        if (decoratorNode && decoratorNode.Child != null) 
        {
            children.Add(decoratorNode.Child);
        }

        return children;
    }
#endif
}
