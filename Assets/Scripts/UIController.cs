using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour {

    private Text startText;

    private void Start()
    {
        startText = gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startText.gameObject.SetActive(false);
        }
    }

}
