using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class PairsParser : MonoBehaviour {
    // Путь к нужному файлу
    private int _countOrderSet { get; set; }

    private List<OrderPair> pairs;
    private List<string> _allNamesElementsOrderSet;
    private short[][] _arrayOrderSet;

    private bool isAddElementInDictionary(string newElementName) {
        /// Функциия принимает название узла (элемента множества) 
        /// Проверяет есть ли такое в словаре 
        /// Если число есть в словаре, то у него уже есть свой индекс
        /// Иначе даем ему уникальный индекс 
        if( _allNamesElementsOrderSet.Contains(newElementName))
            return false;
        
        _allNamesElementsOrderSet.Add(newElementName);
        return true;
        
    }

    private void ParsFile(XDocument xDoc) {
        /// Метод, который считывает пары и записывает их в список

        /// Из файла xDoc выбираются элементы с названием CountOrderSet
        /// Затем считываются их значения и Trim() удаляет пробелы в начале и в конце
        string countValue = xDoc.Root?.Element("CountOrderSet")?.Value?.Trim();
        /// Проверка, можно ли данные спарсить, если да то вернет число, нет то вернет нуль
        this._countOrderSet = int.TryParse(countValue, out int result) ? result : 0;

        if (_countOrderSet == 0)
            Debug.Log("Error[PairsParser.cs]:\n" +
                    "[Количество элементов либо равно нулю, либо в XML файле нет CountOrderSet]");

        /// Перем все пары, которые есть в файла.
        /// Создаем экземпляры класса OrderPir и задаем сразу ему значения
        /// Все объекты помешаем в список
        try {
            this.pairs = xDoc.Descendants("Pair")
                .Select(p => new OrderPair {
                    // Select - преобразует объекты в XML в другой обьект уже в C#
                    // Trim() убирает лишние пробелы вокруг букв " A ", " B " внутри XML
                    Inf = p.Element("inf")?.Value?.Trim(),
                    Sup = p.Element("sup")?.Value?.Trim(),
                    Value = Convert.ToInt16(p.Element("val")?.Value?.Trim())
                }).ToList();
        }
        catch (Exception ex) {
            Debug.Log("Error[PairsParser.cs]:\n" +
                    $"[Не удалось распарсить пары]{ex}");
        }
    }

    private void GetListNameOrderSet() {
        /// Создадим вспомогательный список имен элементов упорядоченного множества
        this._allNamesElementsOrderSet = new();
        short indexLastElement = 0;

        for (int iterListPairs = 0; iterListPairs < this.pairs.Count; iterListPairs++) {
            // Если Элемент не содержится в данном списке, то добавим его и присвоим индекс
            if (isAddElementInDictionary(
                this.pairs[iterListPairs].Inf))
                indexLastElement++;

            if (isAddElementInDictionary(
                this.pairs[iterListPairs].Sup))
                indexLastElement++;
        }

    }

    private void InstallRangePairNode(int iInf, int iSup, short value) {
        /// Устанавливаем свзяь в матрицу и ее виличину
        if (this._arrayOrderSet[iInf][iSup] != 0 && this._arrayOrderSet[iInf][iSup] < 0) 
            Debug.Log("ERROR: [Былы заданы как минемум 2 противоречивые пары (a < b) и (b < a)]");
            Application.Quit();

        this._arrayOrderSet[iInf][iSup] = value;
        this._arrayOrderSet[iSup][iInf] = (short)-value;

    }
    private void InstallRangePairNode(int id) {

        this._arrayOrderSet[id][id] = 1;

    }

    private void ConvertingPairsToMatrix() {
        /// Задаем массив элементов из нулей
        this._arrayOrderSet = new short[_countOrderSet][];
        for (int i = 0; i < _countOrderSet; i++) 
            this._arrayOrderSet[i] = new short[_countOrderSet];
        
        // За множества упорядоченных пар строим матрицу
        foreach (OrderPair pair in pairs) {
            if (pair.Inf != pair.Sup) 
                InstallRangePairNode(
                    _allNamesElementsOrderSet.IndexOf(pair.Inf), 
                    _allNamesElementsOrderSet.IndexOf(pair.Sup),
                    pair.Value);
            
            else 
                InstallRangePairNode(_allNamesElementsOrderSet.IndexOf(pair.Inf));
        }
    }


    public OrderSetInMatrix GetMatrixOrderSetP(XDocument _xDoc) {
        /// Главная функция которая собирает все остальные 
        // Распаршеваем файл и получаем упорядоченные пары.
        ParsFile(_xDoc);

        // Задаем словарь, в котором будут хранится номера элементов в матрице
        GetListNameOrderSet();

        //Создаем матрицу отношений
        ConvertingPairsToMatrix();

        return new (this._arrayOrderSet, this._allNamesElementsOrderSet);

    }

}