using UnityEngine;

public class AnimatronicManagerS : MonoBehaviour
{

    [SerializeField] private GameObject doorSprite1Josh;
    [SerializeField] private GameObject doorSprite2Josh;
    [SerializeField] private GameObject doorSprite1Renee;
    [SerializeField] private GameObject doorSprite2Renee;

    AnimatronicType1 Joshtronic;
    AnimatronicType1 Reneetronic;
    AnimatronicType1 Shelbytronic;





    AnimatronicType2 Soltronic;
    AnimatronicType2 Larktronic;





    AnimatronicType3 Cherrytronic;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {






        Joshtronic = new AnimatronicType1(0, 3, 4, 0, 10, 1000, 3, 1, "Josh", doorSprite1Josh, doorSprite2Josh);
        Reneetronic = new AnimatronicType1(0, 3, 4, 2, 10, 1000, 4, 1, "Renee", doorSprite1Renee, doorSprite2Renee);
        //Shelbytronic = new AnimatronicType1(0, 3, 4, 2, 10, 1000, 4, 1, "Shelby", doorSprite1Renee, doorSprite2Renee);
        //Joshtronic.RandomizeRespawn();
        //Reneetronic.RandomizeRespawn();




        Soltronic = new AnimatronicType2(3, 0, 0, 4, 10, 10000, 5, 1, "Sol");
        Larktronic = new AnimatronicType2(3, 4, 0, 4, 10, 10000, 1, 1, "Lark");
        Soltronic.SetupScripts(Larktronic);
        Larktronic.SetupScripts(Soltronic);
        //Soltronic.RandomizeRespawn();



        Cherrytronic = new AnimatronicType3(0, 0, 0, 0, 10, 500, 0, 1, "Cherry");






    }

    // Update is called once per frame
    private void Update()
    {

        Soltronic.AnimatronicUpdate();
        Larktronic.AnimatronicUpdate();




        Joshtronic.AnimatronicUpdate();
        Reneetronic.AnimatronicUpdate();
        //Shelbytronic.AnimatronicUpdate();




        Cherrytronic.AnimatronicUpdate();






    }
}