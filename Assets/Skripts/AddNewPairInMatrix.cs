using System.Collections.Generic;
using System.Diagnostics;

public class AddNewPairInMatrix {

    private short[][] _matrix { get; set; }
    private List<string> _listNames { get; set; }

    public AddNewPairInMatrix(short[][] matrix, List<string> listName){
        _matrix = matrix;
        _listNames = listName;
    }

    private int FindIndexName(string name) {
        /// Вернет -1 если в списке нет такого имени
        /// Вернет позицию имени если оно есть в списке
        for (int i = 0; i < _listNames.Count; i++)
            if (_listNames[i] == name)
                return i;
        return -1;
    }

    private void AddNameInList(string name) =>
        /// Добавляем новое имя в список имен
        _listNames.Add(name);

    private void InstalInMatrixNewValues(short[][] newMatrix, short newValues, (System.Index, System.Index) indexs) {
        /// Добавляем новую связь в матрицу
        newMatrix[indexs.Item1][indexs.Item2] = newValues;
        newMatrix[indexs.Item2][indexs.Item1] = (short)-newValues;

    }

    public bool AddOneNode(short[][] matrix, string name) {
        /// Если элемента нет в списке, то добавим его, если есть то не добавляем
        int indexElementInList = FindIndexName(name);
        if (indexElementInList < 0) {
            MamipulationByMatrix.AddElementsInMatrix(matrix, 1);
            AddNameInList(name);
        }
        else
            return false;
        return true;
        

    }

    public void Add(string nameNewPair, string namePairSupOrInf, short newValues) {

        /// Метод добавляет в матрицу отношений новую пару и устанавливает между ними связь

        int indexFirstElementInList = FindIndexName(nameNewPair);
        int indexSecondElementInList = FindIndexName(namePairSupOrInf);

        short[][] newMatrix;

        if (indexSecondElementInList < 0 && indexFirstElementInList < 0) {
            newMatrix = MamipulationByMatrix.AddElementsInMatrix(_matrix, 2);
            InstalInMatrixNewValues(newMatrix, newValues, (^1,^0));
            AddNameInList(nameNewPair);
            AddNameInList(namePairSupOrInf);
        }
        else if (indexSecondElementInList < 0 || indexFirstElementInList < 0) {
            newMatrix = MamipulationByMatrix.AddElementsInMatrix(_matrix, 1);
            if (indexSecondElementInList > 0) {
                InstalInMatrixNewValues(newMatrix, newValues, (indexFirstElementInList, ^0));
                AddNameInList(nameNewPair);
            }
            else {
                InstalInMatrixNewValues(newMatrix, newValues, (^0, indexSecondElementInList));
                AddNameInList(namePairSupOrInf);
            }

        }
        else 
            InstalInMatrixNewValues(_matrix, newValues, (indexFirstElementInList, indexSecondElementInList));

    }
    public void Deconstruct(out short[][] newMatrix, out List<string> newListNames) {
        /// Разбираем класс на матрицу и список имен
        newMatrix = _matrix;
        newListNames = _listNames;
    }
}

