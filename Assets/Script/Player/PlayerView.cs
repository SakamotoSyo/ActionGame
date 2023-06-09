using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [Header("TimeのRectTransform")]
    [SerializeField] RectTransform _rectCurrent;
    [Tooltip("Timeバー最長の長さ")]
    private float _maxHpWidth;
    [Tooltip("Timeバーの最大値")]
    private float _maxHp;

    void Awake()
    {
        _maxHpWidth = _rectCurrent.sizeDelta.x;
    }

    public void SetMaxHp(float Maxhp)
    {
        _maxHp = Maxhp;
    }

    public void SetHpCurrent(float currentHp)
    {
        //バーの長さを更新
        _rectCurrent.SetWidth(GetWidth(currentHp));
        if (currentHp < 0)
        {
            Debug.Log("ぽつーん");
        }
    }


    private float GetWidth(float value)
    {
        float width = Mathf.InverseLerp(0, _maxHp, value);
        return Mathf.Lerp(0, _maxHpWidth, width);
    }

}

public static class UIExtensions
{
    /// <summary>
    /// 現在の値をRectにセットする
    /// </summary>
    /// <param name="width"></param>
    public static void SetWidth(this RectTransform rect, float width)
    {
        Vector2 s = rect.sizeDelta;
        s.x = width;
        rect.sizeDelta = s;
    }
}
