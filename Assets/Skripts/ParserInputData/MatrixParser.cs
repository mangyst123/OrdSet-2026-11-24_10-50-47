using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class MatrixPaeser : MonoBehaviour  {
    private List<string> _allNamesElementsOrderSet;
    private short[][] arrayOrderSet;

    private void GetDictByMatrix(int rows, string[] headers) {
        this._allNamesElementsOrderSet = new List<string>();
        for (short i = 0; i < rows; i++)
            this._allNamesElementsOrderSet.Add(headers[i]);
    }

    private string[] GetNamesElements(XElement matrixElement) {
        /// Извлекаем названия столбцов
        return matrixElement
            .Element("headers")
            .Elements("coln")
            .Select(c => c.Value)
            .ToArray();
    }

    private void SetMatrixInData(int rows, int cols, List<XElement> rowElements) {
        /// Из полученного файла считываем матрицу 
        arrayOrderSet = new short[rows][];
        for (int i = 0; i < cols; i++)
            arrayOrderSet[i] = new short[cols];

        for (int i = 0; i < rows; i++) {
            List<XElement> cellElements = rowElements[i].Elements("coln").ToList();
            for (int j = i; j < cols; j++) {
                if (i == j)  arrayOrderSet[i][j] = 1;
                else {
                    short valuePair = short.Parse(cellElements[j].Value);
                    arrayOrderSet[i][j] = valuePair;
                    arrayOrderSet[j][i] = (short)-valuePair;
                }
            }
        }
    }

    public OrderSetInMatrix GetMatrixOrderSetM(XDocument xDoc) {
        /// Метод, который считывает пары и записывает их в список

        // Загрузка XML из файла
        XElement matrixElement = xDoc.Element("ordermatrix");

        // Считываем параметры размеров
        int rows = int.Parse(matrixElement.Attribute("rows").Value);
        int cols = int.Parse(matrixElement.Attribute("colns").Value);

        // Получаем матрицу
        SetMatrixInData(rows, cols, matrixElement.Elements("row").ToList());

        // Получаем имена упорядоченных элементов
        string[] headers = GetNamesElements(matrixElement);

        // Получаем словарь с названиями точек
        GetDictByMatrix(rows, headers);

        // Возвращаем распаршеный файл
        return new(this.arrayOrderSet, this._allNamesElementsOrderSet);
    }
}
