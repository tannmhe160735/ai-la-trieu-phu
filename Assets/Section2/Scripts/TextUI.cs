using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Section2
{
    public class NewBehaviourScript : MonoBehaviour
    {
        public TextMeshProUGUI text;

        // Start is called before the first frame update
        void Start()
        {
            text.text = "Hello World";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
