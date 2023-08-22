using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sources.Behaviour.Extensions
{
    static class EventSystemEx
    {
        public static bool IsCrossPlatformPointerOverGameObject(this EventSystem eventSystem, GraphicRaycaster canvasRaycaster)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(eventSystem);

            Vector2 screenPosition;
            
            if (Input.touchCount > 0)
                screenPosition = Input.GetTouch(0).position;
            else
                screenPosition = Input.mousePosition;

            eventDataCurrentPosition.position = screenPosition;

            List<RaycastResult> results = new List<RaycastResult>();
            canvasRaycaster.Raycast(eventDataCurrentPosition, results);
            
            return results.Count > 0;
        }
    }
}
