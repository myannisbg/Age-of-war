using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
  public static Slider slider;

  public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value= health;

    }

    public void SetHealth(int health)
  {
    slider.value = health;
  }
}
