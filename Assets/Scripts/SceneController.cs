using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2; //how meny cards need
    public const int gridCols = 4;
    public const float offsetX = 3f; //lenght between 
    public const float offsetY = 3.5f;

    [SerializeField] private Memory originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private Text scoreLabel;

    private Memory firstRevealed;
    private Memory secondRevealed;
    private int score = 0;

    private Vector3 startPos;
    private int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };

    void Start()
    {
        startPos = originalCard.transform.position;

        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                Memory card;

                if (i == 0 && j == 0)  // use the original for the first grid space
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as Memory;
                }

                int index = j * gridCols + i; // next card in the list for each grid space
                int id = numbers[index];

                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    public bool canReveal
    {
        get { return secondRevealed == null; }
    }

    private int[] ShuffleArray(int[] numbers) //Knuth algorithm of shuffle array
    {
        int[] newArray = numbers.Clone() as int[];

        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];

            int r = Random.Range(i, newArray.Length);

            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }

    public void CardRevealed(Memory card)
    {
        if (!firstRevealed)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (firstRevealed.id == secondRevealed.id)  // increment score if the cards match
        {
            score++;
            scoreLabel.text = "Score: " + score;
        }
        else // otherwise turn them back over after .5s pause
        {
            yield return new WaitForSeconds(.5f);

            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }

        firstRevealed = null;
        secondRevealed = null;
    }
}
