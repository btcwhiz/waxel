using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string tipToShow;
    private float timeToWait = 0.3f;

    public void OnPointerEnter(PointerEventData pointerEventData) {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        StopAllCoroutines();
        HoverTipManager.OnMousLoseFocus();
    }

    private void ShowMessage() {
        HoverTipManager.OnMouseHover(tipToShow, Input.mousePosition);
    }

    private IEnumerator StartTimer() {
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }
}
