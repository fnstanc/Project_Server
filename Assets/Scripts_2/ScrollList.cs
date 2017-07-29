using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ItemData
{
    public string name;
}

public class ScrollList : MonoBehaviour
{
    Scrollbar bar = null;
    ScrollRect rect = null;
    RectTransform vive = null;
    RectTransform content = null;

    //item
    private GameObject item;
    private float itemHeight = 0;
    private int itemCount = 10;
    private float itemSpacing = 20;
    private List<RectTransform> itemLst = new List<RectTransform>();

    //item起始位置
    private float contentStart = 0;
    //最高y
    private float currMaxY = 0;
    //最低y
    //private float currMinY = 0;
    //数据起始位置
    private int dataStartIndex = 0;
    private int dataEndIndex = 0;

    private void Start()
    {
        initData();
        initLayout();
        initRefresh();
        bar.onValueChanged.AddListener(onBarMove);
    }
    //初始化布局
    void initLayout()
    {
        setListInfo(7, 20);
        rect = this.GetComponent<ScrollRect>();
        bar = rect.verticalScrollbar;
        vive = this.transform.Find("vive").GetComponent<RectTransform>();
        content = this.transform.Find("vive/content").GetComponent<RectTransform>();
        item = this.transform.Find("vive/content/item").gameObject;
        itemHeight = item.GetComponent<RectTransform>().sizeDelta.y;

        for (int i = 0; i < itemCount; i++)
        {
            GameObject go = MonoBehaviour.Instantiate(item, item.transform.parent) as GameObject;
            itemLst.Add(go.GetComponent<RectTransform>());
            go.name = i.ToString();
        }
        itemLst.Add(item.GetComponent<RectTransform>());
        item.gameObject.name = itemCount.ToString();
        float h = dataLst.Count * itemHeight + Mathf.Ceil(vive.sizeDelta.y / (itemHeight + itemSpacing)) * itemSpacing;
        Debug.Log(" dataLst.Coun " + dataLst.Count + "   itemHeight :" + itemHeight + "    h :" + dataLst.Count * itemHeight);
        Debug.Log("vive.sizeDelta.y: " + vive.sizeDelta.y + "       (vive.sizeDelta.y / itemHeight) : " + Mathf.Ceil(vive.sizeDelta.y / (itemHeight + itemSpacing)) + "   itemSpacing:  " + itemSpacing);
        content.sizeDelta = new Vector2(content.sizeDelta.x, h);
        bar.value = 1;
        currVal = bar.value;
        lastVal = bar.value;

        contentStart = content.sizeDelta.y / 2 - itemHeight / 2;
        for (int i = 0; i < itemLst.Count; i++)
        {
            itemLst[i].anchoredPosition = new Vector2(0, contentStart);
            contentStart -= (itemSpacing + itemHeight / 2);
        }

        currMaxY = content.anchoredPosition.y;
        //currMinY = content.anchoredPosition.y + (itemLst.Count - 1) * (itemHeight + itemSpacing) - itemSpacing;
    }

    void initRefresh()
    {
        for (int i = 0; i < itemLst.Count; i++)
        {
            itemLst[i].transform.Find("Text").GetComponent<Text>().text = dataLst[i].name;
        }
        dataStartIndex = 0;
        dataEndIndex = itemCount;
    }


    public float barVal = 1;

    private void Update()
    {
        barVal = bar.value;

    }
    private float currVal = 1;
    private float lastVal = 1;

    private void onBarMove(float val)
    {
        currVal = val;
        if (val >= 1 || val <= 0) return;
        if (currVal > lastVal)
        {
            Debug.Log("向上");
            //当最下面的item超过最高界限 则丢到最下面并刷新数据
            if (currMaxY - content.anchoredPosition.y > 0)
            {
                if (dataStartIndex < 0) return;
                float posY = itemLst[0].anchoredPosition.y + (itemSpacing + itemHeight / 2);
                var go = itemLst[itemCount];
                itemLst.RemoveAt(itemCount);
                itemLst.Insert(0, go);
                go.anchoredPosition = new Vector2(0, posY);
                currMaxY -= (itemHeight / 2 + itemSpacing);
                //currMinY -= (itemHeight / 2 + itemSpacing);
                dataStartIndex--;
                dataEndIndex--;
                itemLst[0].transform.Find("Text").GetComponent<Text>().text = dataLst[dataStartIndex].name;
            }
        }
        else
        {
            Debug.Log("乡下");
            //当最上面的item超过最高界限 则丢到最上面并刷新数据
            if (content.anchoredPosition.y - currMaxY > (itemHeight / 2 + itemSpacing))
            {
                if (dataEndIndex >= dataLst.Count - 1) return;
                //最上面的item已经被移动到边界外了
                //移除最上 添加到最下
                float posY = itemLst[itemCount].anchoredPosition.y - (itemSpacing + itemHeight / 2);
                var go = itemLst[0];
                itemLst.RemoveAt(0);
                itemLst.Add(go);
                go.anchoredPosition = new Vector2(0, posY);
                currMaxY += (itemHeight / 2 + itemSpacing);
                //currMinY += (itemHeight / 2 + itemSpacing);
                dataStartIndex++;
                dataEndIndex++;
                itemLst[itemCount].transform.Find("Text").GetComponent<Text>().text = dataLst[dataEndIndex].name;
            }
        }
        lastVal = currVal;
    }

    private void refreshItemByIndex(GameObject go)
    {

    }


    public void setListInfo(int count, float dis)
    {
        itemCount = count - 1;
        itemSpacing = dis;
    }

    //测试数据
    private List<ItemData> dataLst = new List<ItemData>();
    private void initData()
    {
        for (int i = 0; i < 15; i++)
        {
            ItemData dt = new ItemData();
            dt.name = "名字" + i;
            dataLst.Add(dt);
        }
    }

}
