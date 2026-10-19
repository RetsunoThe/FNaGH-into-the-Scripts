using UnityEngine;

public class SoltronicManager : MonoBehaviour
{

    [SerializeField] private GameObject doorSprite1Josh;
    [SerializeField] private GameObject doorSprite2Josh;
    [SerializeField] private GameObject doorSprite1Renee;
    [SerializeField] private GameObject doorSprite2Renee;

    AnimatronicType1 Joshtronic;
    AnimatronicType1 Reneetronic;





    public AnimatronicType2 Soltronic;
    public AnimatronicType2 Larktronic;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {






        Joshtronic = new AnimatronicType1(0, 3, 4, 0, 10, 1000, 3, 1, "Josh", doorSprite1Josh, doorSprite2Josh);
        Reneetronic = new AnimatronicType1(0, 3, 4, 0, 10, 1000, 4, 1, "Renee", doorSprite1Renee, doorSprite2Renee);
        //Joshtronic.RandomizeRespawn();
        //Reneetronic.RandomizeRespawn();




        Soltronic = new AnimatronicType2(3, 0, 0, 4, 10, 10000, 5, 1, "Sol");
        Larktronic = new AnimatronicType2(3, 4, 0, 4, 10, 10000, 1, 1, "Lark");
        Soltronic.SetupScripts(Larktronic);
        Larktronic.SetupScripts(Soltronic);
        Soltronic.RandomizeRespawn();






    }

    // Update is called once per frame
    private void Update()
    {

        Soltronic.AnimatronicUpdate();
        Larktronic.AnimatronicUpdate();





        Joshtronic.AnimatronicUpdate();
        Reneetronic.AnimatronicUpdate();






    }
}