using System.Collections.Generic;

public class AddNewPair {

    private short[][] _matrix { get; set; }
    private List<string> _listNames { get; set; }

    public AddNewPair(short[][] matrix, List<string> listName){
        _matrix = matrix;
        _listNames = listName;
    }

    private void AddTwoPointsInMatrix(short newValues, (string, string) namesTwoNode) {
        /// Копирую и расширяю матрицу
        _matrix = MamipulationByMatrix.AddElementsInMatrix(_matrix, 2);
        AddNewConnection.InstalInMatrixNewValues(_matrix, newValues, (^1, ^0));

        AddNodeInMatrix.AddNameInList(_listNames, namesTwoNode.Item1);
        AddNodeInMatrix.AddNameInList(_listNames, namesTwoNode.Item2);
    }

    private void AddOnePointInMatrix((int, int) idTwoElements, (string, string) namesTwoNode, short newValues) {
        _matrix = MamipulationByMatrix.AddElementsInMatrix(_matrix, 1);
        if (idTwoElements.Item2 > 0) {
            AddNewConnection.InstalInMatrixNewValues(_matrix, newValues, (idTwoElements.Item1, ^0));
            AddNodeInMatrix.AddNameInList(_listNames, namesTwoNode.Item1);
        }
        else {
            AddNewConnection.InstalInMatrixNewValues(_matrix, newValues, (^0, idTwoElements.Item2));
            AddNodeInMatrix.AddNameInList(_listNames, namesTwoNode.Item2);
        }

    }

    public void Add((string, string) nameInfAndSup, short newValues) {
        /// Метод добавляет в матрицу отношений новую пару и устанавливает между ними связь
        (int, int) idTwoElements = (
            FinIndElInListNames.FindIndexName(_listNames, nameInfAndSup.Item1), 
            FinIndElInListNames.FindIndexName(_listNames, nameInfAndSup.Item2)
            );

        if (idTwoElements.Item2 < 0 && idTwoElements.Item1 < 0)
            AddTwoPointsInMatrix(newValues, (nameInfAndSup.Item1, nameInfAndSup.Item2));
        else if (idTwoElements.Item2 < 0 || idTwoElements.Item1 < 0)
            AddOnePointInMatrix(idTwoElements, (nameInfAndSup.Item1, nameInfAndSup.Item2), newValues);
        else 
            AddNewConnection.InstalInMatrixNewValues(_matrix, newValues, (idTwoElements.Item1, idTwoElements.Item2));

    }
    public void Deconstruct(out short[][] newMatrix, out List<string> newListNames) {
        /// Разбираем класс на матрицу и список имен
        newMatrix = _matrix;
        newListNames = _listNames;
    }
}

