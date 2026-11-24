public struct MatrixPropertiesData {
    /// <summary>
    ///  Хранит информацию о матрице
    ///  Является ли она матрицей отношения порядка
    ///  и является ли она решеткой
    /// </summary>
    public bool IsTransitivMatrix { get; set; }
    public bool IsAntiSemmetrikMatrix { get; set; }
    public bool IsRefleksivMatrix { get; set; }
    public bool IsLattice { get; set; }

    public readonly bool IsOrderMatrix() => 
        /// Является ли матрица матрицей отношения порядка
        (IsRefleksivMatrix && IsAntiSemmetrikMatrix && IsRefleksivMatrix);

    private readonly int GetCountProperties()=>
        /// Считаем количество истинных свойств
        (IsRefleksivMatrix ? 1 : 0) + (IsAntiSemmetrikMatrix ? 1 : 0) 
        + (IsTransitivMatrix ? 1 : 0) + (IsLattice ? 1 : 0);

    public readonly string MessageFormation() {
        /// Полный отчет о параметрах матрицы для Решетки
        return $"Выполнено свойств: {GetCountProperties()} из 4: \n" +
            (IsRefleksivMatrix ? "[Рефлексивность : ДА]": "[Рефлексивность : НЕТ]") + "; " +
            (IsAntiSemmetrikMatrix ? "[Антисимметричность : ДА]" : "[Антисимметричность : НЕТ]") + "; " +
            (IsTransitivMatrix ? "[Транзитивность : ДА]" : "[Транзитивность : НЕТ]") + "; " +
            (IsLattice ? "[Решётка : ДА]" : "[Решётка : НЕТ]");
    }
}
