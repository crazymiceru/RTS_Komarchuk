using UnityEngine;
using UnityEngine.UI;

public class InfoPanelButton : InfoPanelBasic
{
    public Button Button => _button;
    [SerializeField] private Button _button;

}
