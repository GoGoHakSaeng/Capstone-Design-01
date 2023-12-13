using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountryControlPanelChecker : MonoBehaviour
{
    public UnityEvent OnPanelActivated;
    public UnityEvent OnPanelDeactivated;

    private bool isActive = false;

    private void Start()
    {
        DeactivatePanel();
    }

    public void ActivatePanel()
    {
        if (!isActive)
        {
            isActive = true;
            OnPanelActivated.Invoke();
            gameObject.SetActive(true);
        }
    }

    public void DeactivatePanel()
    {
        if (isActive)
        {
            isActive = false;
            OnPanelDeactivated.Invoke();
            gameObject.SetActive(false);
        }
    }
}
