using UnityEngine;
using UnityEngine.UI;

public class Counters : MonoBehaviour
{
    public Text player1Counter;
    public Text player2Counter;
    public GameObject player1Text;
    public GameObject player2Text;

    private int counter1 = 0;
    private int counter2 = 0;
    private void OnEnable()
    {
        Bullet.BallForPlayer1 += Bullet_BallForPlayer1;
        Bullet.BallForPlayer2 += Bullet_BallForPlayer2;
        Bullet.Player1TakePoint += Bullet_Player1TakePoint;
        Bullet.Player2TakePoint += Bullet_Player2TakePoint;
    }

    private void Bullet_BallForPlayer2()
    {
        player1Text.SetActive(false);
        player2Text.SetActive(true);
    }

    private void Bullet_BallForPlayer1()
    {
        player1Text.SetActive(true);
        player2Text.SetActive(false);
    }

    void Start()
    {
        player1Text.SetActive(false);
        player2Text.SetActive(false);
        player1Counter.text = counter1.ToString();
        player2Counter.text = counter2.ToString();
    }

    private void Bullet_Player2TakePoint()
    {
        counter2++;
        player2Counter.text = counter2.ToString();
        Debug.Log("»√–Œ  2 «¿¡»¬¿≈“");
    }

    private void Bullet_Player1TakePoint()
    {
        counter1++;
        player1Counter.text = counter1.ToString();
        Debug.Log("»√–Œ  1 «¿¡»¬¿≈“");
    }


    private void OnDisable()
    {
        Bullet.BallForPlayer1 -= Bullet_BallForPlayer1;
        Bullet.BallForPlayer2 -= Bullet_BallForPlayer2;
        Bullet.Player1TakePoint -= Bullet_Player1TakePoint;
        Bullet.Player2TakePoint -= Bullet_Player2TakePoint;
    }
}
