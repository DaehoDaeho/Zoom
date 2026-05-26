using UnityEngine;

public class LayerTagViewer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string layerName = LayerMask.LayerToName(gameObject.layer);
        string tagName = gameObject.tag;

        Debug.Log(gameObject.name + " / Layer : " + layerName + " / Tag : " + tagName);
    }
}
