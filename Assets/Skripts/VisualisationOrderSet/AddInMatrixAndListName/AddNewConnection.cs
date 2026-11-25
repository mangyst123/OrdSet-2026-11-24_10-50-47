public static class AddNewConnection{
    public static void InstalInMatrixNewValues(
        short[][] newMatrix, short newValues, (System.Index, System.Index) indexs) {
        /// Добавляем новую связь в матрицу
        newMatrix[indexs.Item1][indexs.Item2] = newValues;
        newMatrix[indexs.Item2][indexs.Item1] = (short)-newValues;
    }
}
