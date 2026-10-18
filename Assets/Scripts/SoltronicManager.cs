using UnityEngine;

public class SoltronicManager : MonoBehaviour
{
    public AnimatronicType2 Soltronic;
    public AnimatronicType2 Larktronic;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Soltronic = new AnimatronicType2(0, 3, 4, 0, 10, 5, 0, "Lark");
        Larktronic = new AnimatronicType2(0, 3, 4, 0, 10, 1, 0, "Sol");
        

    }

    // Update is called once per frame
    private void Update()
    {

        Soltronic.AnimatronicUpdate();

    }
}