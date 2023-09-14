using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MoveableObjectInfoPanel : UIPanel
{
    public MoveableObject moveableTarget;
    public RectTransform infoPanel;
    public TextMeshProUGUI infoText;

    public override void OpenPanel()
    {
        Debug.Log("Will open panel...");
        if (moveableTarget != null) 
        {
            Debug.Log("Moveable Target Found...");
            float mass = moveableTarget.GetComponent<Rigidbody2D>().mass;
            infoPanel.position = Camera.main.WorldToScreenPoint(moveableTarget.transform.position);
            infoText.text = mass.ToString("0.0") + "x10<sup>24</sup> kg";
        }

        base.OpenPanel();
    }
    private void FixedUpdate()
    {
        if (_isOpen) 
        {
            infoPanel.position = Camera.main.WorldToScreenPoint(moveableTarget.transform.position);
        }
    }
}
