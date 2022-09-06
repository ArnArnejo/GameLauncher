using UnityEngine;


public class Game : MonoBehaviour
{
    public StoreGame gameDetails;

    

    public virtual void SetupStoreGameDetails(StoreGame storeGame) {
        gameDetails = storeGame;
    }

    
}


