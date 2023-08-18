using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    private Button btn;
    [SerializeField] private RawImage buttonImage;

    private int itemId;
    private Sprite buttonTexture;

    public int ItemId
    {
        set 
        { 
            itemId = value; 
        }
    }

    public Sprite ButtonTexture
    {
        set
        {
            buttonTexture = value;
            buttonImage.texture = buttonTexture.texture;
        }
    }

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SelectObject);
    }

    private void Update()
    {
        if (UIManager.Instance.OnEntered(gameObject))
        {
            //transform.localScale = Vector3.one * 2f;
            transform.DOScale(Vector3.one * 2f, 0.3f);
        }
        else
        {
            //transform.localScale = Vector3.one;
            transform.DOScale(Vector3.one, 0.3f);
        }
    }

    private void SelectObject()
    {
        DataHandler.Instance.SetFurniture(itemId);
    }
}
