using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryControlPanelManager : MonoBehaviour
{

    public CountryControlPanelChecker[] Panels;
    private void Start()
    {
        Debug.Log(Panels);
        foreach (var panel in Panels)
        {
            // Subscribe to panel events
            panel.OnPanelActivated.AddListener(PanelActivated);
            panel.OnPanelDeactivated.AddListener(PanelDeactivated);
        }
    }

    private void PanelActivated()
    {
        Debug.Log("Panel activated");
        // Handle panel activation here
    }

    private void PanelDeactivated()
    {
        Debug.Log("Panel deactivated");
        // Handle panel deactivation here
    }
}
