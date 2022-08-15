using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetSelected : MonoBehaviour
{
    private Button btn;
    private TMP_InputField field;
    private Toggle toggle;


    private void Start()
    {
        btn = GetComponent<Button>();
        field = GetComponent<TMP_InputField>();
        toggle = GetComponent<Toggle>();

        if (btn != null) {
            //When there button proceed here
            btn.onClick.AddListener(()=> TabController.SetSelected(btn as Selectable));
        }
        if (field != null)
        {
            //When there button proceed here
            field.onSelect.AddListener( text => TabController.SetSelected(field as Selectable));
        }
        if (toggle != null)
        {
            //When there button proceed here
            toggle.onValueChanged.AddListener( value => TabController.SetSelected(toggle as Selectable));
        }


    }

    
}
