using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLibrary : MonoBehaviour
{
    object[] library = {
        new { id = 0, textContents = "No Listed Text", containsNext = false},
        new { id = 1, textContents = "Test Element 1", containsNext = false},
        new { id = 2, textContents = "Test Element 2a", containsNext = true},
        new { id = 3, textContents = "Test Element 2b", containsNext = false}
    };


    

}
