using UnityEngine;

public class GameController : MonoBehaviour
{

    
    public static GameController gc;
    private void Awake()
    {
        if (gc == null)
        {
            gc = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
