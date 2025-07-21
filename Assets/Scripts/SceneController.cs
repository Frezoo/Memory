
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    [SerializeField] MemoryCard originalCard;
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] Sprite[] images;

    private int score;
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    private MemoryCard firstRevaled;
    private MemoryCard secondRevaled;

    public bool canRevaled
    {
        get { return secondRevaled == null; }
    }

    private void Start()
    {
        Vector3 startPos = originalCard.transform.position;
        int[] numbers = { 0,0,1,1,2,2,3,3};
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }
                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);


                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }

        
    }

    public void CardRevaled(MemoryCard card)
    {
        if (firstRevaled == null)
        {
            firstRevaled = card;
        }
        else
        {
            secondRevaled = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (firstRevaled.Id == secondRevaled.Id)
        {
            score++;
            scoreLabel.text = $"Score:{score}";
            if (score == 4)
            {
                yield return new WaitForSeconds(1);
                Restart();
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            firstRevaled.Unreval();
            secondRevaled.Unreval();
        }
        firstRevaled = null;
        secondRevaled = null;
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i,newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
