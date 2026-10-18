using UnityEngine;

public class SoltronicManager : MonoBehaviour
{
    public AnimatronicType2 Soltronic;
    public AnimatronicType2 Larktronic;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Larktronic = new AnimatronicType2(0, 3, 4, 0, 20, 1, 0, "Lark");
        Soltronic = new AnimatronicType2(0, 3, 4, 0, 15, 5, 0, "Sol");
        
        Larktronic.SetupScripts(Soltronic);
        Soltronic.SetupScripts(Larktronic);
    }

    // Update is called once per frame
    private void Update()
    {
        Larktronic.AnimatronicUpdate();
        Soltronic.AnimatronicUpdate();
        

    }
}