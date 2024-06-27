using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] BaseCounter clearCounter;
    [SerializeField] GameObject [] visualGameObject;
    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    { 
        if( e.selectedCounter == clearCounter )
        {
            show();
        }
        else
        {
            hide();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void show()
    { foreach( var item in visualGameObject )
        {
            item.SetActive(true);
        }
        
    }
    private void hide()
    {
        foreach (var item in visualGameObject)
        {
            item.SetActive(false);
        }
    }
}
