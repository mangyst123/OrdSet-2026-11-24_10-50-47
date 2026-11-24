public static class RefleksivnostCheck{
    /// Рефлексивная, если на главной диагонали стоят 1
    public static bool IsRefleksivMatrix(short[][] orderMatrix) {
        for (int i = 0; i < orderMatrix.Length; i++)
            if (orderMatrix[i][i] != 1)
                return false;
        return true;
    }
}
