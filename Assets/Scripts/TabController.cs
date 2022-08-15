using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabController : MonoBehaviour
{
    public Selectable[] selectables;
    private static Selectable[] prv_selectables;
    public static int currentSelected;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentSelected = 0;
        prv_selectables = selectables;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            Next();
        }
    }

    private void Next() {

        currentSelected++;

        if (currentSelected >= selectables.Length) {
            currentSelected = 0;
        }

        selectables[currentSelected].Select();
    }
    
    public static void SetSelected(Selectable select) {
        for (int i = 0; i < prv_selectables.Length; i++)
        {
            if (select == prv_selectables[i]) {
                currentSelected = i;
                return;
            }
        }
    }

   
}
