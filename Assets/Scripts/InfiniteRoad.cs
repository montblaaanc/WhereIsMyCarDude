using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class InfiniteRoad : MonoBehaviour
{

    public float scrollSpeed;

    private Vector2 offset;

    private void Update()
    {
        offset = new Vector2(0, Time.time * scrollSpeed); //параметры осей X и Y.

        GetComponent<Renderer>().material.mainTextureOffset = offset; //бесконечная дорога.
    }

}