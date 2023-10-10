using System.Collections;
using UnityEngine;

namespace CoreRuntime.Weapons
{
    [CreateAssetMenu(fileName = "GrappleGun", menuName = "AquaKitty/ScriptableObjects/Weapons/GrappleGun")]
    public class GrappleGun : Weapon
    {
        private GameObject webZip;
        private SpriteRenderer webZipSpriteRenderer;
        private Vector2 webZipStart;
        private Vector2 webZipEnd;
        private float holdDownStartTime;
        private float holdDownTime;
        private float moveStartTime;
        private float moveHoldTime;

        private Vector3 webZipDir;
        private Vector3 webZipTargetPosition;
        private float webZipSpeed;

        public override IEnumerator Shoot(Vector3 mousePos)
        {
            Debug.Log("GrappleGun fired!");
            yield return HandleWebZipStart(mousePos);
        }
        
        private IEnumerator HandleWebZipStart(Vector3 mousePos) {

            GrappleSliderController.Instance.HideSlider();

            holdDownStartTime = Time.time;
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);

            //Debug.Log(hit.collider);
            if (hit.collider != null && hit.collider.CompareTag("Helper"))
            {
                GrappleSliderController.Instance.InitSlider(holdDownStartTime);
                SoundManager.PlaySound("grapple");
                webZipTargetPosition = mousePos;
                webZipDir = (webZipTargetPosition - PlayerController.Instance.transform.position).normalized;

                spawnWebZip(FirePoint.position, FirePoint.rotation);
                Vector3 webDir = (webZipTargetPosition - FirePoint.position).normalized;
                webZip.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(webDir.y, webDir.x) * Mathf.Rad2Deg - 90);

                webZipSpeed = 20.0f;
                
                webZip.transform.position = FirePoint.position;
                webZipSpriteRenderer = webZip.GetComponent<SpriteRenderer>();
                webZipStart = new Vector2(webZip.GetComponent<SpriteRenderer>().size.x, 0);
                webZipEnd = new Vector2(webZip.GetComponent<SpriteRenderer>().size.x,
                    Vector3.Distance(FirePoint.position, webZipTargetPosition));
            
                webZipSpriteRenderer.size = webZipEnd;
            }

            yield return null;
        }

        public IEnumerator HandleWebZipping()
        {
            if (webZipSpriteRenderer == null) yield break;
            //Debug.Log("HandleWebZipping status");
            GrappleSliderController.Instance.HideSlider();
            holdDownTime = Time.time - holdDownStartTime;
            webZipSpriteRenderer.size = Vector2.Lerp(webZipStart, webZipEnd, holdDownTime);

            float travelDistance = Vector2.Distance(PlayerController.Instance.transform.position, webZipTargetPosition);
            if (webZipSpeed * holdDownTime >= travelDistance)
            {
                // Debug.Log("Hold for too long!");
                PlayerController.Instance.transform.position = Vector2.MoveTowards(
                    PlayerController.Instance.transform.position,
                    webZipTargetPosition,
                    webZipSpeed * (travelDistance / webZipSpeed)
                );
            } else
            {
                PlayerController.Instance.transform.position = Vector2.MoveTowards(
                    PlayerController.Instance.transform.position,
                    webZipTargetPosition,
                    webZipSpeed * holdDownTime
                );
            }

            yield return null;
            HandleWebZippingSliding();
        }

        private void HandleWebZippingSliding()
        {
            webZipDir = new Vector3(webZipDir.x, webZipDir.y, 0);
            webZipSpeed -= webZipSpeed * Time.deltaTime * 15.0f;
            PlayerController.Instance.transform.position += webZipDir * webZipSpeed * Time.deltaTime;
            //Debug.Log("webZipSpeed is: " + webZipSpeed);
            SetWebZipEnabled(false);
        }

        public void SetWebZipEnabled(bool enabled)
        {
            if (webZip != null)
            {
                webZip.gameObject.SetActive(enabled);
            }
        }
        
        private void spawnWebZip(Vector2 position, Quaternion rotation)
        {
            if (webZip == null)
            {
                webZip = Instantiate(ammunitionPrefab, position, rotation);
            }
            else
            {
                webZip.transform.position = position;
                webZip.transform.rotation = rotation;
                SetWebZipEnabled(true);
            }
        }
    }
}