using UnityEngine;

public class SetParentsAndChildrans : MonoBehaviour
{
    private void AddParenAndChild(GameObject childrenNode, GameObject parentNode) {
        // Вспомогательная функция
        childrenNode.GetComponent<ParentsAndChildrens>().AddParents(parentNode);
        parentNode.GetComponent<ParentsAndChildrens>().AddChildren(childrenNode);
    }

    public void SetParentAndChildranByTwoNodes(
        (GameObject, GameObject) pairNodeInGame, bool isFirstSmalletSecond) {
        // Зададим для каждого обьекта родителей и детей
        if (isFirstSmalletSecond)
            AddParenAndChild(pairNodeInGame.Item1, pairNodeInGame.Item2);

        else
            AddParenAndChild(pairNodeInGame.Item2, pairNodeInGame.Item1);
    }
}
