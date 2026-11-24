using System.Collections.Generic;

public struct OrderSetInMatrix {
    // Матрица, которая показывает какие элементы сравнимы,
    // т е один больше или меньше другого (1 или -1)
    // 0 - это не сравнимые элементы или одинаковые
    private short[][] _array { get; set; }
    // Имена точек и места где они находятся в матрице
    private List<string> _positionAndNameAllElements {  get; set; }

    public OrderSetInMatrix(short[][] array, List<string>  positionAndNameAllElements ) {
        _array = array;
        _positionAndNameAllElements = positionAndNameAllElements;
    }

    public void SetNewMatrix(short[][] newMatrix) =>
        this._array = newMatrix;
    public void SetNewNames(List<string> newNames) =>
        this._positionAndNameAllElements = newNames;

    private readonly short[][] CreateNewMatrix(int n) {
        short[][] newMatrix = new short[_array.Length + n][];
        for (int i = 0; i < _array.Length + n; i++) {
            newMatrix[i] = new short[_array.Length + n];
            for (int j = 0; j < _array.Length; j++) {
                if (i < _array.Length)
                    newMatrix[i][j] = _array[i][j];
                else
                    newMatrix[i][j] = 0;
                
            }
        }
        return newMatrix;
    }

    private readonly int FindIndexName(string name) {
        for (int i = 0; i < _positionAndNameAllElements.Count; i++) 
            if (_positionAndNameAllElements[i] == name) 
                return i;
        return -1;
    }

    private void AddNameInList(string name) =>
        _positionAndNameAllElements.Add(name);
    

    public void Add(string nameNewPair, string namePairSupOrInf, short newValues) {
        
        int indexFirstElementInList = FindIndexName(nameNewPair);
        int indexSecondElementInList = FindIndexName(namePairSupOrInf);

        short[][] newMatrix;

        if (indexSecondElementInList < 0 && indexFirstElementInList < 0) {
            newMatrix = CreateNewMatrix(2);
            newMatrix[^1][^0] = newValues;
            newMatrix[^0][^1] = (short)-newValues;
            AddNameInList(nameNewPair);
            AddNameInList(namePairSupOrInf);
            _array = newMatrix;
        }
        else if (indexSecondElementInList < 0 || indexFirstElementInList < 0) {
            newMatrix = CreateNewMatrix(1);
            if(indexSecondElementInList > 0) {
                newMatrix[indexFirstElementInList][^0] = newValues;
                newMatrix[^0][indexFirstElementInList] = (short)-newValues;
                AddNameInList(nameNewPair);
            }
            else {
                newMatrix[^0][indexSecondElementInList] = newValues;
                newMatrix[indexSecondElementInList][^0] = (short)-newValues;
                AddNameInList(namePairSupOrInf);
            }

            _array = newMatrix;
        }
        else {
            _array[indexFirstElementInList][indexSecondElementInList] = newValues;
            _array[indexSecondElementInList][indexFirstElementInList] = (short)-newValues;
        }

    }

    public readonly void Delate() {

    }

    public readonly void AddList() {

    }

    public readonly void DelateList() {

    }

    public void SetMatrix(short[][] newMatrix) => _array = newMatrix; 
    public void SetNamesElements(List<string> newListNames) => _positionAndNameAllElements = newListNames;

    public readonly short[][] GetMatrix() => this._array;
    public readonly List<string> GetNamesElements() => this._positionAndNameAllElements;

}
