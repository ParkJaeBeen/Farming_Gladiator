using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Image _slotIcon;
    private RectTransform rcTransform;

    Rect rc;
    public Rect RC
    {
        get
        {
            rc.x = rcTransform.position.x - rcTransform.rect.width * 0.5f;
            rc.y = rcTransform.position.y + rcTransform.rect.height * 0.5f;
            return rc;
        }
    }

    public Image slotIcon
    {
        get { return _slotIcon; }
    }


    public bool IsInRect(Vector2 _pos)
    {
        if (_pos.x >= RC.x &&
            _pos.x <= RC.x + RC.width &&
            _pos.y >= RC.y - RC.height &&
            _pos.y <= RC.y)
            return true;
        return false;
    }

    private void Awake()
    {
        _slotIcon = transform.GetChild(0).GetComponent<Image>();
        rcTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("½½·Ô = " + slotIcon + ", ·ºÆ® = " + rcTransform);

        rc.x = rcTransform.position.x - rcTransform.rect.width * 0.5f;
        rc.y = rcTransform.position.y + rcTransform.rect.height * 0.5f;
        rc.xMin = rc.x;
        rc.yMin = rc.y;
        rc.xMax = rc.x + rcTransform.rect.width;
        rc.yMax = rc.y + rcTransform.rect.height;
        rc.width = rcTransform.rect.width;
        rc.height = rcTransform.rect.height;
    }


    private void Update()
    {
        if (_slotIcon.sprite != null)
            _slotIcon.gameObject.SetActive(true);
        else if (_slotIcon.sprite == null)
            _slotIcon.gameObject.SetActive(false);
    }

}
