using System;

public static class TransitiveCheck
{
    private static short[][] CreateResultMatrix(short[][] a, short[][] b) {
        int n = a.Length;

        short[][] result = new short[n][];
        for (int i = 0; i < n; i++) {
            if (a[i].Length != n || b[i].Length != n) {
                throw new ArgumentException("Обе матрицы должны быть квадратными порядка n.");
            }
            result[i] = new short[n];
        }
        return result;
    }

    private static short[][] MultiplySquareMatrices(short[][] a, short[][] b) {
        /// Возведение матрицы в квадрат или же перемножение 2х матриц
        int n = a.Length;

        short[][] result = CreateResultMatrix(a, b);

        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {

                int sum = 0;
                for (int k = 0; k < n; k++)
                    sum += a[i][k] * b[k][j];

                result[i][j] = (short)sum;
            }
        }
        return result;
    }
    private static bool HaveSamePositivePositions(short[][] quadroM, short[][] originM) {
        /// Проверяем совпадают ли все едицины в M^2 с M 
        for (int i = 0; i < quadroM.Length; i++)
            for (int j = 0; j < quadroM.Length; j++)
                if ((quadroM[i][j] > 0) && (originM[i][j] < 0))
                    return false;

        return true;
    }

    public static bool IsTransitivMatrix(short[][] orderMatrix) {
        /// Проверяем является ли матрица транзитивной
        int countOrder = orderMatrix.Length;

        short[][] quadroMatirx = MultiplySquareMatrices(orderMatrix, orderMatrix);
        return HaveSamePositivePositions(quadroMatirx, orderMatrix);
    }
}
