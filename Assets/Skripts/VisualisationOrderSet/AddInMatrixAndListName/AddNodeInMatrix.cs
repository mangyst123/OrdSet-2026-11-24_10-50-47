
using System.Collections.Generic;

public static class AddNodeInMatrix {
    /// <summary>
    ///  Добавляем новый элемент в матрицу и список имен
    /// </summary>

    public static void AddNameInList(
        List<string> listNames, string name) =>
    /// Добавляем новое имя в список имен
        listNames.Add(name);

    public static bool AddOneNodeInMatrix(
        short[][] matrix, List<string> listNames, string name, int indexElementInList) {
        /// Если элемента нет в списке, то добавим его, если есть то не добавляем
        if (FinIndElInListNames.FindIndexName(listNames,name) < 0) {
            MamipulationByMatrix.AddElementsInMatrix(matrix, 1);
            AddNameInList(listNames, name);
        }
        else
            return false;
        return true;
    }
}

