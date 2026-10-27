using UnityEngine;

public class AnimatronicManagerS : MonoBehaviour
{

    [SerializeField] private GameObject doorSprite1Josh;
    [SerializeField] private GameObject doorSprite2Josh;
    [SerializeField] private GameObject doorSprite1Renee;
    [SerializeField] private GameObject doorSprite2Renee;
    [SerializeField] private GameObject doorSprite1Shelby;
    [SerializeField] private GameObject doorSprite2Shelby;

    AnimatronicType1 Joshtronic;
    AnimatronicType1 Reneetronic;
    AnimatronicType1 Shelbytronic;





    AnimatronicType2 Soltronic;
    AnimatronicType2 Larktronic;





    AnimatronicType3 Cherrytronic;

    GameManagerS gameManager;


    //AnimatronicType4 Lilytronic;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerS>();

        print(gameManager.nightNumber);

        
        if(gameManager.nightNumber == 1)
        {
            Joshtronic = new AnimatronicType1(0, 3, 4, 0, 10, 1000, 3, 1, "Josh", doorSprite1Josh, doorSprite2Josh, "JoshMask");
            Reneetronic = new AnimatronicType1(0, 3, 4, 2, 10, 1000, 4, 1, "Renee", doorSprite1Renee, doorSprite2Renee, "ReneeMask");
        }
        if(gameManager.nightNumber == 2)
        {
            
        }
        if(gameManager.nightNumber == 3)
        {
            
        }
        if(gameManager.nightNumber == 4)
        {

        Joshtronic = new AnimatronicType1(0, 3, 4, 0, 10, 1000, 3, 1, "Josh", doorSprite1Josh, doorSprite2Josh, "JoshMask");
        Reneetronic = new AnimatronicType1(0, 3, 4, 2, 10, 1000, 4, 1, "Renee", doorSprite1Renee, doorSprite2Renee, "ReneeMask");
        Shelbytronic = new AnimatronicType1(0, 3, 4, 2, 10, 1000, 2, 1, "Shelby", doorSprite1Shelby, doorSprite2Shelby, null);
        //Joshtronic.RandomizeRespawn();
        //Reneetronic.RandomizeRespawn();



        Soltronic = new AnimatronicType2(3, 0, 0, 4, 10, 10000, 5, 1, "Sol");
        Larktronic = new AnimatronicType2(3, 4, 0, 4, 10, 10000, 1, 1, "Lark");
        Soltronic.SetupScripts(Larktronic);
        Larktronic.SetupScripts(Soltronic);
        //Soltronic.RandomizeRespawn();



        Cherrytronic = new AnimatronicType3(10, 500, 0, 1, "Cherry");

        }
        if(gameManager.nightNumber == 5)
        {
            
        }



       



        //Lilytronic = new AnimatronicType4();

    }

    // Update is called once per frame
    private void Update()
    {
        if(gameManager.nightNumber == 1)
        {
            Joshtronic.AnimatronicUpdate();
            Reneetronic.AnimatronicUpdate();
        }
        if(gameManager.nightNumber == 2)
        {
            Joshtronic.AnimatronicUpdate();
            Reneetronic.AnimatronicUpdate();

            Soltronic.AnimatronicUpdate();
            Larktronic.AnimatronicUpdate();
        }
        if(gameManager.nightNumber == 3)
        {
            Joshtronic.AnimatronicUpdate();
            Reneetronic.AnimatronicUpdate();

            Soltronic.AnimatronicUpdate();
            Larktronic.AnimatronicUpdate();

            Cherrytronic.AnimatronicUpdate();
        }
        if(gameManager.nightNumber == 4)
        {

            Soltronic.AnimatronicUpdate();
            Larktronic.AnimatronicUpdate();


            Joshtronic.AnimatronicUpdate();
            Reneetronic.AnimatronicUpdate();
            Shelbytronic.AnimatronicUpdate();


            Cherrytronic.AnimatronicUpdate();

        }
        if(gameManager.nightNumber == 5)
        {
            
        }



       



        // Soltronic.AnimatronicUpdate();
        // Larktronic.AnimatronicUpdate();




        // Joshtronic.AnimatronicUpdate();
        // Reneetronic.AnimatronicUpdate();
        // Shelbytronic.AnimatronicUpdate();




        // Cherrytronic.AnimatronicUpdate();






    }
}




