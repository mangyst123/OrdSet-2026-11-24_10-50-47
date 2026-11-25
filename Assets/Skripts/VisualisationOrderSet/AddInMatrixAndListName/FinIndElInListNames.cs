
using System.Collections.Generic;

public static class FinIndElInListNames {
    /// <summary>
    /// Находит позицию имени узла в списке имен
    /// </summary>

    public static int FindIndexName(List<string> listNames, string name) {
        /// Вернет -1 если в списке нет такого имени
        /// Вернет позицию имени если оно есть в списке
        for (int i = 0; i < listNames.Count; i++)
            if (listNames[i] == name)
                return i;
        return -1;
    }
}

