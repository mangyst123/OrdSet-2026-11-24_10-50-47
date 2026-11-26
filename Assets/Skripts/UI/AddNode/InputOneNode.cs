using TMPro;
using UnityEngine;

public class InputOneNode : MonoBehaviour
{
    public void ClickCreateNode() {
        string nameNewNode = GameObject.Find("Name").GetComponent<TMP_InputField>().text;

        if (nameNewNode != "") 
            GetComponent<VisualizerOrdSet>().AddNewZeroNode(nameNewNode);
        
    }
}
