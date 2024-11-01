using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = AFG.Character.CharacterController;

namespace AFG
{
    [CreateAssetMenu(fileName = "BooksData", menuName = "Configs/new BooksData")]
    public class BooksData : ScriptableObject
    {
        [SerializeField] private BooksDataWrapper _booskDataWrapper;
        public BooksDataWrapper BooksDataWrapper => _booskDataWrapper;
    }

    [System.Serializable]
    public class BooksDataWrapper
    {
        [SerializeField] private int _booksCount;
        public int BooksCount => _booksCount;

        

    }
}