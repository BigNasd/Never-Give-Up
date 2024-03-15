using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image myImage;

    public Image BkImage;

    private TMP_Text text;

    public float speed;

    private void Awake()
    {
        myImage = GetComponent<Image>();
        text = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MonoManager.GetInstance().RemoveUpdateListener(Minify);
        MonoManager.GetInstance().AddUpdateListener(Largen);
        MusicManager.GetInstance().PlaySound("Click sounds 11", false, 1f);
        //BkImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MonoManager.GetInstance().RemoveUpdateListener(Largen);
        MonoManager.GetInstance().AddUpdateListener(Minify);
        //BkImage.gameObject.SetActive(false);
    }

    private void Largen()
    {
        text.fontSize = Mathf.Lerp(text.fontSize, 150f, speed * Time.deltaTime);
    }

    private void Minify()
    {
        text.fontSize = Mathf.Lerp(text.fontSize, 100f, speed * Time.deltaTime);
    }
}
