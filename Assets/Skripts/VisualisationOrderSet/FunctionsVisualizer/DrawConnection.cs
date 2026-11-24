using UnityEngine;


public class DrawConnection : MonoBehaviour
{
    [SerializeField] private GameObject _baseLine = null;
    private void SetPositionLine(int indexPosition, (float, float) positionNode, LineRenderer lr) {
        // Для одной из точек линии задает координаты
        // indexPosition - точка на линии 0 - начальная, 1 конечная
        lr.SetPosition(indexPosition, new Vector3(
            positionNode.Item1,
            positionNode.Item2,
            0));
    }

    private LineRenderer SetStartAndEndLine(
        (Transform, Transform) pairNode, GameObject newLine) {
        /// Установим начальные и конечные позиции для линии
        LineRenderer lr = newLine.GetComponent<LineRenderer>();

        SetPositionLine(0,
            (pairNode.Item1.position.x, pairNode.Item1.position.y),
            lr);
        SetPositionLine(1,
            (pairNode.Item2.position.x, pairNode.Item2.position.y),
            lr);

        return lr;
    }

    private GameObject DrawLine(Vector3 position, string name) {
        GameObject newLine = Instantiate(_baseLine, position, Quaternion.identity);
        newLine.name = name;
        return newLine;
    }

    public LineRenderer DrawLineInTwoNodes(
        (string, string) namesTwoNode, (Transform, Transform) transformTwoNode, 
        bool isFirstSmalletSecond) {

        string nameLine;
        if (isFirstSmalletSecond)
            nameLine = namesTwoNode.Item1 + '|' + namesTwoNode.Item2;
        else
            nameLine = namesTwoNode.Item2 + '|' + namesTwoNode.Item1;

        GameObject newLine = DrawLine(new Vector3(0, 0, 0), nameLine);

        LineRenderer lr = SetStartAndEndLine(transformTwoNode, newLine);

        if (isFirstSmalletSecond)
            // Задаем эту линию как дочернюю для twoNode
            newLine.transform.SetParent(transformTwoNode.Item2);
        else
            newLine.transform.SetParent(transformTwoNode.Item1);

        return lr;

    }
}
