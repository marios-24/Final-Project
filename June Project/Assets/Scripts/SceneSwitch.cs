using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{
   public void StartTutorialScene()
   {
      SceneManager.LoadScene("Tutorial");
   }

   public void StartOpenWorldScene()
   {
      SceneManager.LoadScene("Game");
   }

   public void StartGameModeScene()
   {
      SceneManager.LoadScene("GameMode");
   }
}
