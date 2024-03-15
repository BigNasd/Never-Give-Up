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
    //当自己淡出后执行的委托函数
    private UnityAction hideCallBack;
    //按技能按钮把该技能传入委托
    public UnityAction selectSkill;
    
    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();//得到挂载的组件
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
                //让管理器删除自己
                hideCallBack?.Invoke();
            }
        }
    }
    /// <summary>
    /// 显示选择技能面板
    /// </summary>
    public virtual void ShowMe()
    {
        isShow = true;
        canvasGroup.alpha = 0;
    }
    /// <summary>
    /// 隐藏选择技能面板
    /// </summary>
    public virtual void HideMe(UnityAction callBack)
    {
        isShow = false;
        //canvasGroup.alpha = 1;
        hideCallBack = callBack;
    }
}
