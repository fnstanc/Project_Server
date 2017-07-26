using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PolyImage : Image, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.ToString());
        Debug.Log("click img");
    }
    /// <summary>
    /// 1 -10 -20
    /// 2 10 -20
    /// 3 20 0
    /// 4 10 20
    /// 5 -10 20
    /// 6-20 0
    /// </summary>
    /// <param name="vh"></param>
    private float pi = 3.1415f;
    private float rid = 30;
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        var r = GetPixelAdjustedRect();
        var v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);

        Color32 color32 = color;
        vh.Clear();
        //vh.AddVert(new Vector3(v.x, v.y), color32, new Vector2(0f, 0f));
        //vh.AddVert(new Vector3(v.x, v.w), color32, new Vector2(0f, 1f));
        //vh.AddVert(new Vector3(v.z, v.w), color32, new Vector2(1f, 1f));
        //vh.AddVert(new Vector3(v.z, v.y), color32, new Vector2(1f, 0f));
        //int v1x = (int)(rid * Math.Cos(240  * pi / 180));
        //int v1y = (int)(rid * Math.Sin(240 * pi / 180));
        //int v2x = (int)(rid * Math.Cos(300 * pi / 180));
        //int v2y = (int)(rid * Math.Sin(300 * pi / 180));
        //int v3x = (int)(rid * Math.Cos(0 * pi / 180));
        //int v3y = (int)(rid * Math.Sin(0 * pi / 180));
        //int v4x = (int)(rid * Math.Cos(60 * pi / 180));
        //int v4y = (int)(rid * Math.Sin(60 * pi / 180));
        //int v5x = (int)(rid * Math.Cos(120 * pi / 180));
        //int v5y = (int)(rid * Math.Sin(120 * pi / 180));
        //int v6x = (int)(rid * Math.Cos(180 * pi / 180));
        //int v6y = (int)(rid * Math.Sin(180 * pi / 180));


        vh.AddVert(new Vector3((float)(rid * Math.Cos(240 * pi / 180)), (float)(rid * Math.Sin(240 * pi / 180))), color32, new Vector2(0.25f, 0f));
        vh.AddVert(new Vector3((float)(rid * Math.Cos(300 * pi / 180)), (float)(rid * Math.Sin(300 * pi / 180))), color32, new Vector2(0.75f, 0f));
        vh.AddVert(new Vector3((float)(rid * Math.Cos(360 * pi / 180)), (float)(rid * Math.Sin(360 * pi / 180))), color32, new Vector2(1f, 0.5f));
        vh.AddVert(new Vector3((float)(rid * Math.Cos(60 * pi / 180)), (float)(rid * Math.Sin(60 * pi / 180))), color32, new Vector2(0.75f, 1f));
        vh.AddVert(new Vector3((float)(rid * Math.Cos(120 * pi / 180)), (float)(rid * Math.Sin(120 * pi / 180))), color32, new Vector2(0.25f, 1f));
        vh.AddVert(new Vector3((float)(rid * Math.Cos(180 * pi / 180)), (float)(rid * Math.Sin(180 * pi / 180))), color32, new Vector2(0f, 0.5f));

        //vh.AddVert(new Vector3(v1x, v1y), color32, new Vector2(0.25f, 0f));
        //vh.AddVert(new Vector3(v2x, v2y), color32, new Vector2(0.75f, 0f));
        //vh.AddVert(new Vector3(v3x, v3y), color32, new Vector2(1f, 0.5f));
        //vh.AddVert(new Vector3(v4x, v4y), color32, new Vector2(0.75f, 1f));
        //vh.AddVert(new Vector3(v5x, v5y), color32, new Vector2(0.25f, 1f));
        //vh.AddVert(new Vector3(v6x, v6y), color32, new Vector2(0f, 0.5f));

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 4);
        vh.AddTriangle(4, 5, 0);
        vh.AddTriangle(0, 2, 4);
    }


}
