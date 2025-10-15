using UnityEngine;

public class UIPanel : MonoBehaviour
{

    public virtual void SetupPanel()
    {

    }

    public virtual void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public virtual void ClosePanel()
    {
        gameObject.SetActive(false);

    }
}
