using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;
    public Node Node;
    public Port Input;
    public Port Output;

    public NodeView(Node node) 
    {
        Node = node;
        title = node.name;
        viewDataKey = node.Guid;

        style.left = node.Position.x;
        style.top = node.Position.y;

        CreateInputPorts();
        CreateOutputPorts();
    }

    /// <summary>
    ///Input�p��Port���쐬����
    /// </summary>
    private void CreateInputPorts() 
    {
        if (Node is ActionNode)
        {
            Input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if (Node is ConditionNode)
        {
            Input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
        }

        if (Input != null) 
        {
            Input.portName = "";
            inputContainer.Add(Input);
        }
    }

    /// <summary>
    /// Output�p��Port���쐬����
    /// </summary>
    private void CreateOutputPorts() 
    {
        //���ꂼ���OutputPort���쐬
        if (Node is ActionNode)
        {
          
        }
        else if (Node is ConditionNode) 
        {
            Output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }
        else if (Node is RootNode)
        {
            Output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
        }

        if ( Output != null)
        {
            Output.portName = "";
            outputContainer.Add(Output);
        }
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Node.Position.x = newPos.xMin;
        Node.Position.y = newPos.yMin;
    }

    public override void OnSelected()
    {
        Debug.Log("�Ă΂ꂽ");
        base.OnSelected();
        if (OnNodeSelected != null) 
        {
            OnNodeSelected.Invoke(this);
        }
    }
}
