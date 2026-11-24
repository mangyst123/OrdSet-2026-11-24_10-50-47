using UnityEngine;

public class DrawPairNode : MonoBehaviour
{
    /// <summary>
    /// Класс для отрисовки пары точек
    /// </summary>
    // Базовая точка, которая будет копироваться
    [SerializeField] private GameObject _baseNode = null;

    private GameObject CreateNewNode(Vector3 position, string name) {
        GameObject newNode = Instantiate(_baseNode, position, Quaternion.identity);
        newNode.name = name;
        return newNode;
    }

    private GameObject FindOrCreateNode(Vector3 position, string name) {
        // Ищет точку, если ее нет то создает в заданной позиции
        GameObject node = GameObject.Find(name);
        if (node == null) {
            node = CreateNewNode(position, name);
            node.transform.SetParent(gameObject.transform);
        }
        return node;
    }

    public (GameObject, GameObject) DrawPairNodes(string nameFirstNode, string nameSecondNode,
        (Vector3, Vector3) coordinatsTwoNodes) {
        /// Нарисуем пару точек со связью

        GameObject nodeOne = FindOrCreateNode(coordinatsTwoNodes.Item1, nameFirstNode);
        GameObject nodeTwo = FindOrCreateNode(coordinatsTwoNodes.Item2, nameSecondNode);

        return (nodeOne, nodeTwo);
    }
}
