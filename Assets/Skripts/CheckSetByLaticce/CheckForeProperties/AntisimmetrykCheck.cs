public static class AntisimmetrykCheck {
    /// Является ли матрица анти-симметричной
    /// Для этого смотрим противоположные позиции 
    /// если они противоположние то матрица Антисиметрична
    public static bool IsAntiSemmetrikMatrix(short[][] orderMatrix) {
        for (int i = 0; i < orderMatrix.Length; i++)
            for (int j = i + 1; j < orderMatrix.Length; j++)
                if (orderMatrix[i][j] != -orderMatrix[j][i])
                    return false;
        return true;
    }
}

