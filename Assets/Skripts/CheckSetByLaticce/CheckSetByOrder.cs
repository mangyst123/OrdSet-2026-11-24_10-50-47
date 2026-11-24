public static class CheckSetByOrder{
    /// <summary>
    /// Класс для проверки множества на 4 свойства
    /// 1) Транзитивность (возводим матрицу в квадрат и смотрим где совпадают положитеьные числа
    ///    у квадрата матрицы и обычной матрицы)
    /// 2) Антисемметричность (На противоположных позициях стоят противоположные значения (-10 и 10))
    /// 3) Рефлексивность (на главной диагонали 1)
    /// 4) Проверка на решотку (если все 3 верны, то проверяем решетку)
    ///     -) Для этого нужно проверить что любые 2 элемента можно сравнить 
    /// </summary>
    /// <param name="orderMatrix"></param>
    /// <returns></returns>

    public static MatrixPropertiesData PropertiesMatrix(short[][] matrix) {

        MatrixPropertiesData properties = new();

        properties.IsTransitivMatrix = TransitiveCheck.IsTransitivMatrix(matrix);
        properties.IsAntiSemmetrikMatrix = AntisimmetrykCheck.IsAntiSemmetrikMatrix(matrix);
        properties.IsRefleksivMatrix = RefleksivnostCheck.IsRefleksivMatrix(matrix);

        properties.IsLattice = properties.IsOrderMatrix() ? LatticeCheck.IsLattice(matrix) : false;

        return properties;
    }
    
}
