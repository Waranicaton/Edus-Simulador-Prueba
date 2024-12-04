using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Multiline()]
    public string content;

    private static LTDescr delay;
    public string header;
   


    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        delay = LeanTween.delayedCall(0.5f, () =>
        {
            ToolTipSystem.Show(content, header);
        }
        );
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        ToolTipSystem.Hide();
      
        
    }

}
