using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class ParentsAndChildrens : MonoBehaviour {

    /// <summary>
    ///  Класс хранит в себе информацию о больших/родителях и меньших/детях элементах
    ///  данной точки
    ///  Так же хранит слой на котором стоит та или иная точка
    /// </summary>

    // Списки элементов которые больше и меньше соответственно
    [SerializeField] private List<GameObject> _pointsParents = new();
    [SerializeField] private List<GameObject> _pointsChildrens = new();
    public ushort layer { get; private set; }

    // Geters and Seters
    public List<GameObject> GetParents => _pointsParents;
    public List<GameObject> GetChildrans => _pointsChildrens;
    public void SetParents(List<GameObject> pointsParents) => _pointsParents = pointsParents;
    public void SetChildrens(List<GameObject> pointsChildrens) => _pointsChildrens = pointsChildrens;
    public void SetLayer(ushort layer) => this.layer = layer; 

    public void AddParents(GameObject parent) => _pointsParents.Add(parent);
    public void AddChildren(GameObject child) => _pointsChildrens.Add(child);
}
