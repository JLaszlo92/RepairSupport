using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetVaLueFromDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    public void GetDropdownValue()
    {
        int pickedEntryIndex = dropdown.value;
        string selectedOption = dropdown.options[pickedEntryIndex].text;
        Debug.Log(selectedOption);
    }

}