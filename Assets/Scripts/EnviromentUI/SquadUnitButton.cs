using System;
using System.Collections;
using System.Collections.Generic;
using AFG;
using UnityEngine;

public class SquadUnitButton : MonoBehaviour
{
    [SerializeField] private Transform _unitHolder;
    private void OnMouseDown()
    {
        GameController.
            Instance.
            PlayerCharactersHolderModel.
            StartCharacterSelection(_unitHolder);
    }
}
