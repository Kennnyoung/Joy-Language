using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetBlankPosition : MonoBehaviour
{
    public Text textComp;
    public Canvas canvas;
    public float VerticalOffSet;
    public float HorizontalOffSet;

    public Vector3 GetPos()
    {
        string text = textComp.text;

        int charIndex = text.IndexOf('_');
        Vector3 avgPosOne = GetAvgPos(charIndex);
        charIndex = text.LastIndexOf('_');
        Vector3 avgPosTwo = GetAvgPos(charIndex);
        return GetWorldPos(0.5f * (avgPosOne + avgPosTwo) / canvas.scaleFactor);
    }

    Vector3 GetAvgPos(int charIndex)
    {
        string text = textComp.text;

        TextGenerator textGen = new TextGenerator(text.Length);
        Vector2 extents = textComp.gameObject.GetComponent<RectTransform>().rect.size;
        textGen.Populate(text, textComp.GetGenerationSettings(extents));

        int newLine = text.Substring(0, charIndex).Split('\n').Length - 2;
        int whiteSpace = text.Substring(0, charIndex).Split(' ').Length - 1;
        int indexOfTextQuad = (charIndex * 4) + (newLine * 4) - 4;
        if (indexOfTextQuad < textGen.vertexCount)
        {
            Vector3 avgPos = (textGen.verts[indexOfTextQuad].position +
                textGen.verts[indexOfTextQuad + 1].position +
                textGen.verts[indexOfTextQuad + 2].position +
                textGen.verts[indexOfTextQuad + 3].position) / 4f;

            return avgPos;
        }
        else
        {
            Debug.LogError("Out of text bound");
            return new Vector3(0,0,0);
        }
    }

    Vector3 GetWorldPos(Vector3 middleUnderScore)
    {
        Vector3 worldPos = textComp.transform.TransformPoint(middleUnderScore);
        worldPos.y += VerticalOffSet;
        worldPos.x += HorizontalOffSet;
        return (worldPos);
        //new GameObject("point").transform.position = worldPos;
        //Debug.DrawRay(worldPos, Vector3.up, Color.red, 50f);
    }

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 100, 80), "Test"))
    //    {
    //        GetPos();
    //    }
    //}
}
