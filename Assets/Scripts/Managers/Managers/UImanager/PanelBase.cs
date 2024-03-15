using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class PanelBase : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float showSpeed = 4f;
    public bool isShow;
    public bool isHideHalf;
    //���Լ�������ִ�е�ί�к���
    private UnityAction hideCallBack;
    //�����ܰ�ť�Ѹü��ܴ���ί��
    public UnityAction selectSkill;
    
    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();//�õ����ص����
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isShow && canvasGroup.alpha != 1)
        {
            if(!isHideHalf)
                canvasGroup.alpha += showSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1 && !isHideHalf)
            {
                canvasGroup.alpha = 1;
            }
        }
        else if(!isShow)
        {
            canvasGroup.alpha -= showSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                //�ù�����ɾ���Լ�
                hideCallBack?.Invoke();
            }
        }
    }
    /// <summary>
    /// ��ʾѡ�������
    /// </summary>
    public virtual void ShowMe()
    {
        isShow = true;
        canvasGroup.alpha = 0;
    }
    /// <summary>
    /// ����ѡ�������
    /// </summary>
    public virtual void HideMe(UnityAction callBack)
    {
        isShow = false;
        //canvasGroup.alpha = 1;
        hideCallBack = callBack;
    }
}
