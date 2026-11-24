using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllColors : MonoBehaviour
{
    ///  Цвет для не активных точек.
    [SerializeField] public Color32 _noActiveNodeOfNoConnection= new (0, 0, 0, 255);
    [SerializeField] public Color32 _noActiveNodeConectionUpAndDown = new (242, 170, 132, 255);
    [SerializeField] public Color32 _noActiveNodeConectionUpOrDown = new (117, 103, 89, 255);

    ///  Цвет для активных точек.
    [SerializeField] public Color32 _activeNodeOfNoConnection = new(255, 0, 255, 255);
    [SerializeField] public Color32 _activeNodeConectionUpAndDown = new (76, 76, 76, 255);
    [SerializeField] public Color32 _activeNodeConectionUpOrDown = new (117, 103, 89, 255);

    private Color32 ColorNode(ParentsAndChildrens parametrsNode) {
        if (parametrsNode.GetChildrans.Count == 0 || parametrsNode.GetParents.Count == 0)
            return _noActiveNodeConectionUpOrDown;
        else if (parametrsNode.GetChildrans.Count == 0 && parametrsNode.GetParents.Count == 0)
            return _noActiveNodeOfNoConnection;
        else if (parametrsNode.GetChildrans.Count != 0 && parametrsNode.GetParents.Count != 0)
            return _noActiveNodeConectionUpAndDown;
        return _activeNodeOfNoConnection;
    }

    public void SetColorsNodes(List<GameObject> listAllNodesInFild) {
        foreach (GameObject node in listAllNodesInFild) {
            node.GetComponent<SpriteRenderer>().color = ColorNode(node.GetComponent<ParentsAndChildrens>());
        }
    }
}
