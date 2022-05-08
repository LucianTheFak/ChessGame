using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlight : MonoBehaviour
{
    public GameObject highlightPrefab;
    private List<GameObject> highlightList;

    private void Start()
    {
        highlightList = new List<GameObject>();
    }

    private GameObject GetHighlightObject()
    {
        GameObject go = highlightList.Find(g => !g.activeSelf);

        if (!go)
        {
            go = Instantiate(highlightPrefab);
            highlightList.Add(go);
        }

        return go;
    }

    public void ShowHighlights(bool[,] moves)
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {
                    GameObject go = GetHighlightObject();
                    go.SetActive(true);
                    go.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
                }
            }
        }
    }

    public void HideHighlights()
    {
        foreach (GameObject go in highlightList) go.SetActive(false);
    }
}
