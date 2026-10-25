using UnityEngine;
using UnityEngine.InputSystem;

public class EInteract : MonoBehaviour
{
    public GameObject eButton;
    private bool playerInRange;
    public GameObject item;

    public PlayerHealth health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Update()
    {
        if (playerInRange &&
              Keyboard.current != null &&
              Keyboard.current.eKey.wasPressedThisFrame)
        {
            
            PerformAction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            eButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            eButton.SetActive(false);
        }
    }

    private void PerformAction()
    {
        if (item.name.Equals("med"))
        {
            item.SetActive(false);
            Destroy(eButton);
            Destroy(gameObject);

            health.SetHealth(10);
        }
        else if (item.name.Equals("ClinicNote"))

        {
            if (item.activeSelf)
            {
                item.SetActive(false);
                Debug.Log("ClinicPanel is now inactive");
            }
            else
            {

                item.SetActive(true);
                Debug.Log("ClinicPanel is now active");

            }

        }
        else if (item.name.Equals("TabletPanel"))
        {
            if (item.activeSelf)
            {
                item.SetActive(false);
                Debug.Log("TabletPanel is now inactive");
            }
            else
            {
                item.SetActive(true);
                Debug.Log("TabletPanel is now active");
            }
        }
        else
        {

        }
    }
}

