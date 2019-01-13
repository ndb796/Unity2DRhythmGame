using System.Collections;
using UnityEngine;

public class NoteController : MonoBehaviour {

    public GameObject[] Notes;
    public GameObject gameManager;

    int bpm = 160;
    int divider = 30;
    float beatCount;
    float beatInterval;
    float startingPoint = 3.5f;

    private Note[] notes = new Note[] {
        new Note(0, 2),
        new Note(3, 6),
        new Note(1, 10),
        new Note(2, 14),
        new Note(3, 24),
        new Note(0, 30),
        new Note(1, 40),
        new Note(2, 48),
        new Note(3, 54),
        new Note(0, 60),
        new Note(0, 64),
        new Note(2, 64),
        new Note(1, 66),
        new Note(3, 66),
        new Note(0, 68),
        new Note(2, 68),
        new Note(1, 70),
        new Note(3, 70),
        new Note(0, 72),
        new Note(2, 72),
        new Note(1, 74),
        new Note(3, 74),
        new Note(0, 76),
        new Note(2, 76),
        new Note(1, 78),
        new Note(3, 78),
        new Note(0, 80),
        new Note(2, 80),
        new Note(1, 82),
        new Note(3, 82),
        new Note(0, 84),
        new Note(2, 84),
        new Note(1, 86),
        new Note(3, 86),
        new Note(0, 88),
        new Note(2, 88),
        new Note(1, 90),
        new Note(3, 90),
        new Note(0, 92),
        new Note(2, 92),
        new Note(1, 94),
        new Note(3, 94),
        new Note(0, 96),
        new Note(2, 96),
        new Note(1, 98),
        new Note(3, 98),
        new Note(0, 100),
        new Note(2, 100),
        new Note(1, 102),
        new Note(3, 102),
        new Note(0, 103),
        new Note(2, 104),
        new Note(1, 105),
        new Note(3, 106),
        new Note(0, 107),
        new Note(2, 108),
        new Note(1, 109),
        new Note(3, 110),
        new Note(0, 111),
        new Note(2, 112),
        new Note(1, 113),
        new Note(3, 114),
        new Note(1, 115),
        new Note(3, 116),
        new Note(0, 117),
        new Note(2, 118),
        new Note(1, 119),
        new Note(3, 120),
        new Note(0, 121),
        new Note(2, 122),
        new Note(1, 123),
        new Note(3, 124),
        new Note(1, 125),
        new Note(3, 126),
        new Note(0, 127),
        new Note(2, 128),
        new Note(1, 129),
        new Note(3, 130),
        new Note(0, 131),
        new Note(2, 132),
        new Note(1, 133),
        new Note(3, 134),
        new Note(1, 135),
        new Note(3, 136),
        new Note(0, 137),
        new Note(2, 138),
        new Note(1, 139),
        new Note(3, 140),
        new Note(0, 141),
        new Note(2, 142),
        new Note(3, 143),
        new Note(3, 144),
        new Note(1, 145),
        new Note(3, 146),
        new Note(2, 147),
        new Note(2, 148),
        new Note(0, 149),
        new Note(3, 150),
        new Note(0, 151),
        new Note(2, 152),
        new Note(3, 153),
        new Note(3, 154),
        new Note(1, 155),
        new Note(3, 156),
        new Note(2, 157),
        new Note(2, 158),
        new Note(0, 159),
        new Note(3, 160),
        new Note(0, 161),
        new Note(2, 162),
        new Note(3, 163),
        new Note(3, 164),
        new Note(1, 165),
        new Note(3, 166),
        new Note(2, 167),
        new Note(2, 168),
        new Note(0, 169),
        new Note(3, 170),
        new Note(0, 171),
        new Note(2, 172),
        new Note(3, 173),
        new Note(3, 174),
        new Note(1, 175),
        new Note(3, 176),
        new Note(2, 177),
        new Note(2, 178),
        new Note(0, 179),
        new Note(2, 180),
        new Note(3, 181),
        new Note(1, 182),
        new Note(2, 183),
        new Note(2, 184),
        new Note(1, 185),
        new Note(3, 186),
        new Note(2, 187),
        new Note(2, 188),
        new Note(0, 189),
        new Note(3, 190),
        new Note(3, 191),
        new Note(1, 192),
        new Note(2, 193),
        new Note(2, 194),
        new Note(1, 195),
        new Note(3, 196),
        new Note(2, 197),
        new Note(2, 198),
        new Note(0, 199),
        new Note(3, 200),
        new Note(3, 201),
        new Note(1, 202),
        new Note(2, 203),
        new Note(1, 204),
        new Note(1, 205),
        new Note(3, 206),
        new Note(2, 207),
        new Note(2, 208),
        new Note(0, 209),
        new Note(3, 210),
        new Note(3, 211),
        new Note(1, 212),
        new Note(2, 213),
        new Note(3, 214),
        new Note(1, 215),
        new Note(3, 216),
        new Note(2, 217),
        new Note(2, 218),
        new Note(0, 219),
        new Note(3, 220),
        new Note(3, 221),
        new Note(1, 222),
        new Note(2, 223),
        new Note(3, 224),
        new Note(1, 225),
        new Note(3, 226),
        new Note(2, 227),
        new Note(2, 228),
        new Note(0, 229),
        new Note(3, 230),
        new Note(3, 231),
        new Note(1, 232),
        new Note(2, 233),
        new Note(3, 234),
        new Note(1, 235),
        new Note(3, 236),
        new Note(2, 237),
        new Note(2, 238),
        new Note(0, 239),
        new Note(3, 240),
    };

    void Start()
    {
        beatCount = (float) bpm / divider;
        beatInterval = 1 / beatCount;
        for (int i = 0; i < notes.Length; i++)
        {
            StartCoroutine(AwaitMakeNote(notes[i]));
        }
        StartCoroutine(AwaitGameResult(notes[notes.Length - 1].order));
    }

    class Note
    {
        public int noteType;
        public int order;
        public Note(int noteType, int order)
        {
            this.noteType = noteType;
            this.order = order;
        }
    }

    void MakeNote(int noteType)
    {
        Instantiate(Notes[noteType]);
    }

    IEnumerator AwaitMakeNote(Note note)
    {
        int noteType = note.noteType;
        int order = note.order;
        yield return new WaitForSeconds(startingPoint + order * beatInterval);
        MakeNote(noteType);
    }

    IEnumerator AwaitGameResult(int order)
    {
        yield return new WaitForSeconds(startingPoint + order * beatInterval);
        GameResult();
    }

    void GameResult()
    {
        Debug.Log("Finish");
    }

    void Update () {
		
	}
}
