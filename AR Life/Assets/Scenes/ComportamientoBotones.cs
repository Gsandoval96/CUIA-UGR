using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class ComportamientoBotones : MonoBehaviour
{

    private int animalActivo = -1;
    private string saveGatoLevel = "0";
    private string saveGatoExperiencia = "0";
    private float saveGatoProgreso = 0;


    public AudioSource audioCon;
    public Slider expbar;
    public Text expText;
    public Text levelNumber;

    void start (){

    }

    public void JugarBtn(){
      SceneManager.LoadScene("MascotaGato");
    }

    public void MainBtn(){
      SceneManager.LoadScene("MenuPrincipal");
    }
    public void OptionBtn(){
      SceneManager.LoadScene("Opciones");
    }

    public void subirLevel(){
        expbar.value += 5;
        expbar.value = expbar.value % 50;
        expText.text  = expbar.value + "/50";

        if( expbar.value == 0){
          levelNumber.text =  int.Parse(levelNumber.text)+1 + "";
        }

    }


    public void TocarBtn(AudioClip sonidoAreproducir){
      if(animalActivo == 1)
        audioCon.clip = sonidoAreproducir;
        if(sonidoAreproducir.name == "Meow"){
          audioCon.PlayDelayed(1.12f);
        }else{
          audioCon.Play();
        }
//        audioCon.PlayOneShot(sonidoAreproducir);
    }

    void Update ()
    {
      StateManager sm = TrackerManager.Instance.GetStateManager();
      IEnumerable<TrackableBehaviour> tbs = sm.GetActiveTrackableBehaviours();

      foreach(TrackableBehaviour tb in tbs)
      {
        string name = tb.TrackableName;

          ImageTarget it = tb.Trackable as ImageTarget;
          Vector2 size = it.GetSize ();
          if( name == "gato_648"){
            animalActivo = 1;

          } else if( name == "lobo_960"){
            animalActivo = 2;
            limpiarExperiencia();
          }else if( name == "alien_840"){
            animalActivo = 3;
            limpiarExperiencia();
          }else{
            animalActivo = -1;
            limpiarExperiencia();
          }

          Debug.Log ("Active image target:" + name + "  -size: " + size.x + ", " + size.y);

      }
    }

    private void limpiarExperiencia(){
      levelNumber.text = "0";
      expText.text = "0";
      expbar.value = 0;
    }
}
