using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeiSerMove : MonoBehaviour
{
    public Transform bag;

    private Vector3 p0;
    private Vector3 p1;
    private Vector3 p2;

    private void Start()
    {
        BeiSerMove.createAnim(this.gameObject);
    }


    private float timer = 0;
    private bool isRun = false;
    void Update()
    {
        if (!isRun) return;
        timer += Time.deltaTime * 0.9f;
        if (timer <= 1)
            this.transform.position = (1 - timer) * (1 - timer) * p0 + 2 * timer * (1 - timer) * p1 + timer * timer * p2;

        if (timer > 4)
        {
            doAnim();
            //释放资源
        }
    }

    private void doAnim()
    {
        timer = 0;
        this.transform.localScale = Vector3.zero;
        this.transform.localPosition = new Vector3(25, -110, 0);
        p0 = this.transform.position;
        p2 = bag.position;
        p1 = new Vector3(p0.x + 250, p0.y, p0.z);
        isRun = false;
        Sequence seq = DOTween.Sequence();
        seq.Append(this.transform.DOMoveX(p0.x + 100, 1).SetEase(Ease.InCubic).OnComplete(
            () => { DOTween.To((val) => { }, 1, 2, 0.1f).OnComplete(() => { p0 = this.transform.position; isRun = true; }); }));
        seq.Join(this.transform.DOScale(Vector3.one, 1f));
    }

    public static void createAnim(GameObject go)
    {
        BeiSerMove anim = go.GetComponent<BeiSerMove>();
        if (anim == null)
        {
            anim = go.AddComponent<BeiSerMove>();
        }
        if (anim != null)
        {
            anim.doAnim();
        }
    }
}
