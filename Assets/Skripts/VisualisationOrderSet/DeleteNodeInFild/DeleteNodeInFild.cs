using UnityEngine;
using System.Collections.Generic;

public class DeleteNodeInFild : MonoBehaviour
{
    private void DelConnChild(List<GameObject> listChildrans, GameObject node) {
        /// Удалим все соединения с детьми
        foreach (GameObject childran in listChildrans) {
            Destroy(GameObject.Find(childran.name + '|' + node.name));        
            childran.GetComponent<ParentsAndChildrens>().DelateParent(node);
        }
    }

    private void DelConnParents(List<GameObject> listParents, GameObject node) {
        /// Удалим все соединения с родителями
        foreach (GameObject parent in listParents) {
            parent.GetComponent<ParentsAndChildrens>().DelateChildran(node);
            Destroy(GameObject.Find(node.name + '|' + parent.name));
        }
    }

    private void DelAllConnsNode(GameObject node) {
        /// Удаляем все вязи
        DelConnChild(node.GetComponent<ParentsAndChildrens>().GetChildrans, node);
        DelConnParents(node.GetComponent<ParentsAndChildrens>().GetParents, node);
    }
    private bool IsNodeInFild(GameObject node) =>
         GameObject.Find(node.name) != null ? true : false;

    public void DeleteOneNode(GameObject node) {
        if (IsNodeInFild(node)) { 
            DelAllConnsNode(node);
            Destroy(node);
        }
        else 
            Debug.Log($"Вы ввели неверную точку{node.name}, я не могу ее найти...");
        
    }
}
