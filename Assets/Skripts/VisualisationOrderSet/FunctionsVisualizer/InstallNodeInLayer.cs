using System.Collections.Generic;
using UnityEngine;

public class InstallNodeInLayer : MonoBehaviour
{
    /// Характеристики виализации
    // Расстояние межу точками
    [SerializeField] private ushort _startLayer = 0;
    private List<GameObject> _listAllNodesInFild = new();

    private Vector3 _coordinatsFirstPoints;
    private Vector3 _space;

    public void StartParametrs((float, float) spaceXandY, Vector3 coordinatsFirstPoints) {
        /// Задаем параметры из основного класса
        _space = new Vector3(spaceXandY.Item1, spaceXandY.Item2, 0);
        _coordinatsFirstPoints = coordinatsFirstPoints;
    }

    private List<GameObject> FindSmalledNodes() {
        /// Найдем все точки которые не имеют детей
        /// то есть наименьшие точки 
        List<GameObject> nodeInZeroChildren = new();
        foreach (GameObject node in _listAllNodesInFild) {
            if (node.GetComponent<ParentsAndChildrens>().GetChildrans.Count == 0) {
                nodeInZeroChildren.Add(node);
                node.GetComponent<ParentsAndChildrens>().SetLayer(_startLayer);
            }
        }
        return nodeInZeroChildren;
    }


    private void SetLayerParentsNode(List<GameObject> parents, ushort layer) {
        /// Установим уровни/слои для каждой точки
        /// Алгоритм берет родителей предыдушей точки и задает им слой на 1 больше, 
        /// затем обращается к их родителям и т пока все точки не будут иметь свой слой
        if (parents.Count != 0) {
            foreach (GameObject parent in parents) {

                if (parent.GetComponent<ParentsAndChildrens>().layer < layer)
                    parent.GetComponent<ParentsAndChildrens>().SetLayer(layer);

                SetLayerParentsNode(
                    parent.GetComponent<ParentsAndChildrens>().GetParents,
                    (ushort)(parent.GetComponent<ParentsAndChildrens>().layer + 1));
            }
        }
    }

    private void SetLayerAllNodes(List<GameObject> nodeInZeroChildren) {
        /// Для всех начальных/наименьших точек вычисляем родителей и передаем в рекурсивную функцию
        /// которая по очереди определит слои у всех выше расположенных(т е больших) точек
        foreach (GameObject smallNode in nodeInZeroChildren) {
            SetLayerParentsNode(
                smallNode.GetComponent<ParentsAndChildrens>().GetParents,
                (ushort)(_startLayer + 1));
        }
    }
    private void SetPositionParents(List<GameObject> parents) {
        /// Рекурсивная функция устанавливает точки в соответствии с их слоями
        if (parents.Count != 0) {
            foreach (GameObject parent in parents) {
                parent.transform.position = _coordinatsFirstPoints +
                    _space * parent.GetComponent<ParentsAndChildrens>().layer;

                SetPositionParents(
                    parent.GetComponent<ParentsAndChildrens>().GetParents);
            }
        }
    }

    private void SetPositionByLayersAllNodes(List<GameObject> nodeInZeroChildren) {
        /// Аналогично начинает ставить наименьшие точки, а затем переходит к большим
        /// запуская рекурсивный алгоритм
        foreach (GameObject smallNode in nodeInZeroChildren) {

            smallNode.transform.position = _coordinatsFirstPoints;
            SetPositionParents(
                smallNode.GetComponent<ParentsAndChildrens>().GetParents);
        }
    }

    public void InstallNodeInPosition(List<GameObject> listAllNodesInFild) {
        /// Устанавливает все точки на своих позициях в сформированном множестве

        _listAllNodesInFild = listAllNodesInFild;

        Dictionary<string, bool> thisGameObjectSetPosition = new();
        List<GameObject> nodeInZeroChildren = FindSmalledNodes();

        SetLayerAllNodes(nodeInZeroChildren);
        SetPositionByLayersAllNodes(nodeInZeroChildren);
    }
}
