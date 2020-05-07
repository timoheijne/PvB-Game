using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Shared
{
    public class ImageFade : MonoBehaviour
    {
        [SerializeField] private float _blinkSpeed = 1;
        private Image _image;
   
        private void Start()
        {
            _image = GetComponent<Image>();
            StartCoroutine(FadeImage(true, true));
        }
 
        IEnumerator FadeImage(bool fadeAway, bool repeat)
        {
            do
            {
                if (fadeAway)
                {  // fade from opaque to transparent
                    // loop over 1 second backwards
                    for (float i = 1; i >= 0; i -= Time.deltaTime * _blinkSpeed)
                    {
                        // set color with i as alpha
                        _image.color = new Color(1, 1, 1, i);
                        yield return null;
                    }
                }
                else
                { // fade from transparent to opaque
                    // loop over 1 second
                    for (float i = 0; i <= 1; i += Time.deltaTime * _blinkSpeed)
                    {
                        // set color with i as alpha
                        _image.color = new Color(1, 1, 1, i);
                        yield return null;
                    }
                }

                fadeAway = !fadeAway;
            } while (repeat);
        }
    }
}