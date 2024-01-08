using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    [System.Serializable]
    public class soal
    {
        [System.Serializable]
        public class pertanyaan
        {
            [TextArea]
            public string soal;
            public string[] jawaban;
            public int jawabanBenar;
        }

        public pertanyaan elementsPertanyaan;
    }
    
    public List<soal> elementsSoal;
}
