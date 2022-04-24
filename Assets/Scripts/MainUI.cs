using UnityEngine.UI;
using UnityEngine;

public class MainUI : MonoBehaviour{
    
    public GameObject player;
    public Text amphorasTxt;
    
    private int amphoras;
    
    void Start(){
        amphoras = player.GetComponent<PlayerScript>().amphorasPicked;
        
    }
    
    void Update(){
        amphorasTxt.text = "Amphoras : " + amphoras;
    }
}
