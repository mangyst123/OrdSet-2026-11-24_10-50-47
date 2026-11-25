
using System.Collections.Generic;
public class DeleteNode {
    private void CompressMatrix(short[][] matrix, (int, int) ijdElement) {
        /// Убирает из матрицы строку и столбец
        short[][] newMatrix = new short[matrix.Length-1][];
        for (int i = 0; i < matrix.Length-1; i++) {
            newMatrix[i] = new short[matrix.Length - 1];
            for (int j = 0; j < matrix.Length - 1; j++) 
                if (i != ijdElement.Item1 && j != ijdElement.Item2) 
                    newMatrix[i][j] = newMatrix[i][j];
        }
        matrix = newMatrix;
    }
    private bool DeleteNameNode(List<string> listNames, string name) =>
        listNames.Remove(name);
    
    public void DeleteOneNode(short[][] matrix, List<string> listNames, (int,int) ijdElement, string name) {
        CompressMatrix(matrix, ijdElement);
        DeleteNameNode(listNames, name);
    }

}

