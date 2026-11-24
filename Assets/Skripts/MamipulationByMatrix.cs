public static class MamipulationByMatrix {

    /// Производит манипуляции с матрицами

    public static short[][] AddElementsInMatrix(short[][] matrix, int countNewElements) {
        /// Создает новую матрицу большего размера и переписывая старую в нутрь ее
        short[][] newMatrix = new short[matrix.Length + countNewElements][];
        for (int i = 0; i < matrix.Length + countNewElements; i++) {
            newMatrix[i] = new short[matrix.Length + countNewElements];
            for (int j = 0; j < matrix.Length; j++) {
                if (i < matrix.Length)
                    newMatrix[i][j] = matrix[i][j];
                else
                    newMatrix[i][j] = 0;

            }
        }
        return newMatrix;
    }


}
