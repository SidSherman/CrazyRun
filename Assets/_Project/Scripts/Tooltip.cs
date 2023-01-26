
using TMPro;
using UnityEngine;


public class Tooltip : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _tooltipTextMesh;
    [SerializeField] private string _tooltip;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_tooltipTextMesh)
                _tooltipTextMesh.text = _tooltip;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(_tooltipTextMesh)
                _tooltipTextMesh.text = "";
        }
    }
    
}
