using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VisualizerOrdSet : MonoBehaviour {

    [SerializeField] private string _filePath =
    "C:\\Users\\Mangust\\Documents\\GitHub\\Diplom\\Diplom2-0\\Assets\\InputData\\OrderedPairs.xml";

    [SerializeField] private float _delayBetweenSpawns = 0.2f;
    [SerializeField] private float _spaceByNodeX = 2f;
    [SerializeField] private float _spaceByNodeY = 2f;
    [SerializeField] private Vector3 _firstPointCoordinat = new(0, 0, 0);
    [SerializeField] private bool _parseFileOrGenerate = true;
    [SerializeField] private ushort _orderGenerateLattec = 5;

    private OrderSetInMatrix _ordSet = new();
    // Все связи которые есть, надо для обновления линий
    private List<KeyValuePair<GameObject, GameObject>> _connections = new ();
    private List<GameObject> _listAllNodesInFild = new ();
    private List<LineRenderer> _lineRenderers = new ();
    private bool isFirstSmalletSecond;
    private InstallNodeInLayer _layerVusualayzer;

    private void Start()
    {
        if (_parseFileOrGenerate)
            ParseFile();
        else
            CreateOrderSet();

        OutInsormationByOrderMatrix();

        SetStartepParametrsInLayers();

        // Позволяет раз в несколько кадров отрисовывать точку и связи
        // Для красоты
        StartCoroutine(SpawnPointsRoutine());
    }
    private void Update() {
        if (_connections.Count == 0) return;

        // Каждый раз обновляем позицию связей
        for (int k = 0; k < _connections.Count; k++) {
            KeyValuePair<GameObject, GameObject> pair = _connections[k];

            // Обновляем позиции линий визуально
            _lineRenderers[k].SetPosition(0, pair.Key.transform.position);
            _lineRenderers[k].SetPosition(1, pair.Value.transform.position);
        }
    }

    private void CreateOrderSet() {
        short[][] matrix;
        List<string> nameList;
        (matrix, nameList) = GenerateLattise.GetRandomLattice(_orderGenerateLattec);
        _ordSet.SetMatrix(matrix);
        _ordSet.SetNamesElements(nameList);
    }

    private void OutInsormationByOrderMatrix() =>
        /// Выводит характеристики множества по матрице
        Debug.Log(CheckSetByOrder.PropertiesMatrix(_ordSet.GetMatrix()).MessageFormation());
    

    private void ParseFile() {
        /// Парсим данные из входного файла
        if (!File.Exists(_filePath)) return;

        Parser parser = new();
        OrderSetInMatrix _ordSet = parser.GetMatrixOrderSetByFile(_filePath);

    }
    private void SetStartepParametrsInLayers() {
        _layerVusualayzer = gameObject.GetComponent<InstallNodeInLayer>();
        _layerVusualayzer.StartParametrs((_spaceByNodeX, _spaceByNodeY), _firstPointCoordinat);
    }

    private void SetConectionInList((GameObject, GameObject) pairNodeInGame) {
        /// выписываем новую связь в общий список
        if (isFirstSmalletSecond) 
            _connections.Add(new KeyValuePair<GameObject, GameObject>(
                pairNodeInGame.Item1, pairNodeInGame.Item2));
        else
            _connections.Add(new KeyValuePair<GameObject, GameObject>(
                pairNodeInGame.Item2, pairNodeInGame.Item1));       
    }

    private bool IsFirstSmalletSecond((int, int) indexPair) =>
        // Если 1 то тогда значит левая точка больше правой
        _ordSet.GetMatrix()[indexPair.Item1][indexPair.Item2] == 1 ? true : false;


    private (GameObject, GameObject) DrawPairNodes((int,int) indexPairInListNames) =>
        /// Отрисовывает на поле 2 узла
        GetComponent<DrawPairNode>().DrawPairNodes(
                            _ordSet.GetNamesElements()[indexPairInListNames.Item1],
                            _ordSet.GetNamesElements()[indexPairInListNames.Item2],
                        (_firstPointCoordinat, _firstPointCoordinat));
    
    private LineRenderer DrawPairConnection(
        (GameObject, GameObject) pairNodeInGame) =>
        /// Отписовывает на поле связь между 2-я узлами
        /// возвращает сслыку на их связь
        GetComponent<DrawConnection>().DrawLineInTwoNodes(
            (pairNodeInGame.Item1.name, pairNodeInGame.Item2.name),
            (pairNodeInGame.Item1.transform, pairNodeInGame.Item2.transform), 
            isFirstSmalletSecond);

    private void SetParentsAndChildran(
        (GameObject, GameObject) pairNodeInGame) =>
        /// Задает родителя одному обьекту и ребенка другому
        /// Кто больше тот родитель, кто меньше ребенок
        GetComponent<SetParentsAndChildrans>().
            SetParentAndChildranByTwoNodes(pairNodeInGame, isFirstSmalletSecond);

    private void FindAllNodesInFild() {
        // Находим все узлы которые мы создали в уникальном варианте
        _listAllNodesInFild = new();
        foreach (string name in _ordSet.GetNamesElements())
            _listAllNodesInFild.Add(GameObject.Find(name));
    }
    private void SetLayerInFild() =>
        _layerVusualayzer.InstallNodeInPosition(_listAllNodesInFild);

    private void SetColorNodes() => 
        gameObject.GetComponent<AllColors>().SetColorsNodes(_listAllNodesInFild);
    private IEnumerator SpawnPointsRoutine() {
        /// Основный алгоритм визуализации множества
        /// Он Последовательно запускает процесс создания для каждой точки 
        int countNodes = _ordSet.GetMatrix().Length;
        GetComponent<DrawPairNode>().enabled = false;
        GetComponent<DrawConnection>().enabled = false;

        for (int row = 0; row < countNodes; row++) {
            for (int coln = row + 1; coln < countNodes; coln++) {

                if (_ordSet.GetMatrix()[row][coln] != 0) {
                    (GameObject, GameObject) pairNodeInGame = DrawPairNodes((row,coln));
                    isFirstSmalletSecond = IsFirstSmalletSecond((row, coln));
                    SetParentsAndChildran(pairNodeInGame);
                    _lineRenderers.Add(DrawPairConnection(pairNodeInGame));

                    SetConectionInList(pairNodeInGame);

                    yield return new WaitForSeconds(_delayBetweenSpawns);
                }
            }
            yield return new WaitForSeconds(_delayBetweenSpawns);
        }

        FindAllNodesInFild();

        SetLayerInFild();

        SetColorNodes();

    }
}
