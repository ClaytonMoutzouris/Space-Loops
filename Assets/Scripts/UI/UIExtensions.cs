using UnityEngine;
using UnityEngine.UI;

public static class ScrollRectExtensions
{
    //This might only work for vertical atm
    public static Vector2 GetSnapToPositionToBringChildIntoViewVertical(this ScrollRect instance, RectTransform child)
    {
        Canvas.ForceUpdateCanvases();
        Vector2 viewportLocalPosition = instance.viewport.localPosition;
        Vector2 childLocalPosition = child.localPosition;
        Vector2 result = new Vector2(
            0 - (viewportLocalPosition.x + childLocalPosition.x),
            0 - (viewportLocalPosition.y + childLocalPosition.y + child.sizeDelta.y / 2)
        );
        return result;
    }

    public static Vector2 GetSnapToPositionToBringChildIntoViewHorizontal(this ScrollRect instance, RectTransform child)
    {
        Canvas.ForceUpdateCanvases();
        Vector2 viewportLocalPosition = instance.viewport.localPosition;
        Vector2 childLocalPosition = child.localPosition;
        Vector2 result = new Vector2(
            0 - (viewportLocalPosition.x + childLocalPosition.x + child.sizeDelta.x),
            0 - (viewportLocalPosition.y + childLocalPosition.y)
        );
        return result;
    }

    public static void ScrollRepositionY(this ScrollRect instance, RectTransform obj, int sensitivity = 10)
    {
        Canvas.ForceUpdateCanvases();

        var objPosition = (Vector2)instance.transform.InverseTransformPoint(obj.position);
        var scrollHeight = instance.GetComponent<RectTransform>().rect.height;
        var objHeight = obj.rect.height;

        if (objPosition.y > scrollHeight / 2 - sensitivity / 2)
        {
            instance.content.localPosition = new Vector2(instance.content.localPosition.x,
                instance.content.localPosition.y - objHeight - sensitivity);
        }

        if (objPosition.y < -scrollHeight / 2 + sensitivity / 2)
        {
            instance.content.localPosition = new Vector2(instance.content.localPosition.x,
            instance.content.localPosition.y + objHeight + sensitivity);
        }
    }

    public static void ScrollRepositionX(this ScrollRect instance, RectTransform obj)
    {
        Canvas.ForceUpdateCanvases();

        var objPosition = (Vector2)instance.transform.InverseTransformPoint(obj.position);
        var scrollWidth = instance.GetComponent<RectTransform>().rect.width;
        var objWidth = obj.rect.width;

        if (objPosition.x > scrollWidth / 2 - 15)
        {
            instance.content.localPosition = new Vector2(instance.content.localPosition.x - objWidth - 10,
                instance.content.localPosition.y);
        }

        if (objPosition.x < -scrollWidth / 2 + 15)
        {
            instance.content.localPosition = new Vector2(instance.content.localPosition.x + objWidth + 10,
            instance.content.localPosition.y);
        }
    }

    // Shared array used to receive result of RectTransform.GetWorldCorners
    static Vector3[] corners = new Vector3[4];

    /// <summary>
    /// Transform the bounds of the current rect transform to the space of another transform.
    /// </summary>
    /// <param name="source">The rect to transform</param>
    /// <param name="target">The target space to transform to</param>
    /// <returns>The transformed bounds</returns>
    public static Bounds TransformBoundsTo(this RectTransform source, Transform target)
    {
        // Based on code in ScrollRect's internal GetBounds and InternalGetBounds methods
        var bounds = new Bounds();
        if (source != null)
        {
            source.GetWorldCorners(corners);

            var vMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var vMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            var matrix = target.worldToLocalMatrix;
            for (int j = 0; j < 4; j++)
            {
                Vector3 v = matrix.MultiplyPoint3x4(corners[j]);
                vMin = Vector3.Min(v, vMin);
                vMax = Vector3.Max(v, vMax);
            }

            bounds = new Bounds(vMin, Vector3.zero);
            bounds.Encapsulate(vMax);
        }
        return bounds;
    }

    /// <summary>
    /// Normalize a distance to be used in verticalNormalizedPosition or horizontalNormalizedPosition.
    /// </summary>
    /// <param name="axis">Scroll axis, 0 = horizontal, 1 = vertical</param>
    /// <param name="distance">The distance in the scroll rect's view's coordiante space</param>
    /// <returns>The normalized scoll distance</returns>
    public static float NormalizeScrollDistance(this ScrollRect scrollRect, int axis, float distance)
    {
        // Based on code in ScrollRect's internal SetNormalizedPosition method
        var viewport = scrollRect.viewport;
        var viewRect = viewport != null ? viewport : scrollRect.GetComponent<RectTransform>();
        var viewBounds = new Bounds(viewRect.rect.center, viewRect.rect.size);

        var content = scrollRect.content;
        var contentBounds = content != null ? content.TransformBoundsTo(viewRect) : new Bounds();

        var hiddenLength = contentBounds.size[axis] - viewBounds.size[axis];
        return distance / hiddenLength;
    }

    /// <summary>
    /// Scroll the target element to the vertical center of the scroll rect's viewport.
    /// Assumes the target element is part of the scroll rect's contents.
    /// </summary>
    /// <param name="scrollRect">Scroll rect to scroll</param>
    /// <param name="target">Element of the scroll rect's content to center vertically</param>
    public static void ScrollToCenter(this ScrollRect scrollRect, RectTransform target, RectTransform.Axis axis = RectTransform.Axis.Vertical)
    {
        // The scroll rect's view's space is used to calculate scroll position
        var view = scrollRect.viewport ?? scrollRect.GetComponent<RectTransform>();

        // Calcualte the scroll offset in the view's space
        var viewRect = view.rect;
        var elementBounds = target.TransformBoundsTo(view);

        // Normalize and apply the calculated offset
        if (axis == RectTransform.Axis.Vertical)
        {
            var offset = viewRect.center.y - elementBounds.center.y;
            var scrollPos = scrollRect.verticalNormalizedPosition - scrollRect.NormalizeScrollDistance(1, offset);
            scrollRect.verticalNormalizedPosition = Mathf.Clamp(scrollPos, 0, 1);
        }
        else
        {
            var offset = viewRect.center.x - elementBounds.center.x;
            var scrollPos = scrollRect.horizontalNormalizedPosition - scrollRect.NormalizeScrollDistance(0, offset);
            scrollRect.horizontalNormalizedPosition = Mathf.Clamp(scrollPos, 0, 1);
        }
    }

}
