
using System.Collections.Generic;

public static class LatticeCheck {
    /// Является ли матрица решеткой. Сложный и комплексный алгоритм
    /// Для этого необходимо проверить что все элементы сравнимы,
    /// даже те у кого нет прямой связи.
    /// Проходимся по всем элементам над главной диагональю и проверяем условие
    /// что есть супремум или инфинум у этой пары
    private static bool HasSupremum(short[][] matrix, int n, int a, int b) {
        // Находим все общие верхние грани (пересечение строк a и b)
        var upperBounds = new List<int>();
        for (int k = 0; k < n; k++) {
            if (matrix[a][k] == 1 && matrix[b][k] == 1)
                upperBounds.Add(k);
        }

        if (upperBounds.Count == 0) return false;

        // Ищем среди верхних граней наименьшую (она должна быть <= всех остальных из списка)
        foreach (int candidate in upperBounds) {
            bool isLeast = true;
            foreach (int other in upperBounds) {
                if (matrix[candidate][other] != 1) {
                    isLeast = false;
                    break;
                }
            }
            if (isLeast) return true; // Супремум найден
        }

        return false;
    }

    private static bool HasInfimum(short[][] matrix, int n, int a, int b) {
        // Находим все общие нижние грани (пересечение столбцов a и b)
        var lowerBounds = new List<int>();
        for (int k = 0; k < n; k++) {
            if (matrix[k][a] == 1 && matrix[k][b] == 1)
                lowerBounds.Add(k);
        }

        if (lowerBounds.Count == 0) return false;

        // Ищем среди нижних граней наибольшую (все остальные из списка должны быть <= её)
        foreach (int candidate in lowerBounds) {
            bool isGreatest = true;
            foreach (int other in lowerBounds) {
                if (matrix[other][candidate] != 1) {
                    isGreatest = false;
                    break;
                }
            }
            if (isGreatest) return true; // Инфимум найден
        }

        return false;
    }

    public static bool IsLattice(short[][] orderMatrix) {

        int n = orderMatrix.Length;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                if (!HasSupremum(orderMatrix, n, i, j) || !HasInfimum(orderMatrix, n, i, j))
                    return false;
        return true;

    }
}