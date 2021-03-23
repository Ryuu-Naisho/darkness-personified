using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //GUI TEXT MESH PRO



public class NC_GUI : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI hint_textMesh;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    ///<summary>Toggle the visibility of the textmesh, using the panel active state.</summary>
    ///<param name="child">Text mesh</param>
    ///<param name="toggle">Boolean toggle true or false.</param>
    private void ToggleTextMeshPanel(TextMeshProUGUI child, bool toggle)
    {
        var panel = child.transform.parent.gameObject;
        panel.SetActive(toggle);
    }


    ///<summary>Set text content to the TextMeshPro.</summary>
    ///<param name="textMesh">TextMeshPro</param>
    ///<param name="text">String, text content to be added.</summary>
    private void SetText(TextMeshProUGUI textMesh, string text)
    {
        textMesh.text = text;
    }


    ///<summary>Clear the text in the TextMeshPro.</summary>
    private void ClearText(TextMeshProUGUI textMesh)
    {
        textMesh.text = " ";
    }


    ///<summary>Call SetText, using hint_textMesh as textMesh param and hint as text.</summary>
    ///<param name="hint">String hint content.</param>
    public void DisplayHint(string hint)
    {
        SetText(hint_textMesh, hint);
    }


    ///<summary>Call ClearText, using hint_textMesh as textMesh param.</summary>
    public void ClearHint()
    {
        ClearText(hint_textMesh);
    }
}
