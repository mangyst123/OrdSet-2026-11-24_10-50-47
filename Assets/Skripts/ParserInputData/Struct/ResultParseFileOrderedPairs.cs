using System.Collections.Generic;

public struct OrderSetInMatrix {
    /// <summary>
    /// Матрица, которая показывает какие элементы сравнимы,
    /// т е один больше или меньше другого (1 или -1)
    /// 0 - это не сравнимые элементы или одинаковые
    /// </summary>
    private short[][] _array { get; set; }

    /// Имена точек и места где они находятся в матрице
    private List<string> _positionAndNameAllElements {  get; set; }

    public OrderSetInMatrix(short[][] array, List<string>  positionAndNameAllElements ) {
        _array = array;
        _positionAndNameAllElements = positionAndNameAllElements;
    }

    public void SetNewMatrix(short[][] newMatrix) =>
        this._array = newMatrix;
    public void SetNewNames(List<string> newNames) =>
        this._positionAndNameAllElements = newNames;


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
