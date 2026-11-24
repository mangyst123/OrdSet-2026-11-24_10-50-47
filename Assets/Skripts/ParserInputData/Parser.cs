using System;
using System.Xml.Linq;
using UnityEngine;

public class Parser : MonoBehaviour {

    private XDocument _xDoc { get; set; }

    private void ReadFile(string filePath) {
        // При объявлении эземпляра класса, считывает данные из файла
        try {
            _xDoc = XDocument.Load(filePath);
        }
        catch {
            Debug.Log($"Файл [{filePath}] не получилось обработать, увы..");
        }
    }

    public OrderSetInMatrix GetMatrixOrderSetByFile(string filePath) {

        ReadFile(filePath);

        string rootName = _xDoc.Root.Name.LocalName;
        MatrixPaeser _matrixParser = new();
        PairsParser _pairsPaeser = new();

        // Определяем нужный парсер по имени корневого тега
        return rootName switch {
            "ordermatrix" => _matrixParser.GetMatrixOrderSetM(_xDoc),  // Первая структура
            "orderpaits" => _pairsPaeser.GetMatrixOrderSetP(_xDoc), // Вторая структура
            _ => throw new NotSupportedException(
                $"Структура XML с корневым тегом <{rootName}> не поддерживается.")
        };
    }

}

