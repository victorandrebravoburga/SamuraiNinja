using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NewNPCDialogue : MonoBehaviour
{
    public GameObject dialogueBox;      // El cuadro de diálogo (UI Panel)
    public Text dialogueText;           // Texto que mostrará el diálogo
    public string[] dialogueLines;      // Array de líneas de diálogo
    private int currentLineIndex = 0;   // Índice actual del diálogo
    private bool playerInRange = false; // Verifica si el jugador está en rango
    private bool isDialogueActive = false; // Indica si el diálogo está en progreso
    public GameObject player;           // Referencia al jugador (para deshabilitar el movimiento)

    private PlayerState playerState; // Referencia al script del jugador

    private void Start()
    {
        // Asegurarnos de obtener el componente PlayerState del jugador
        if (player != null)
        {
            playerState = player.GetComponent<PlayerState>();
            if (playerState == null)
            {
                Debug.LogError("PlayerState no encontrado en el objeto del jugador.");
            }
        }
        else
        {
            Debug.LogError("El jugador no está asignado en el inspector.");
        }
    }

    private void Update()
    {
        // Si el jugador está en rango y presiona "E", continúa el diálogo
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isDialogueActive)
        {
            // Activa el cuadro de diálogo si aún no está activo
            dialogueBox.SetActive(true);
            isDialogueActive = true;

            // Congela el mundo y el movimiento del jugador
            FreezeWorld();

            // Comienza a mostrar texto
            StartCoroutine(Typing(dialogueLines[currentLineIndex]));
            Debug.Log("Interacción con NPC iniciada.");
        }
        else if (isDialogueActive && Input.GetKeyDown(KeyCode.E) && dialogueText.text == dialogueLines[currentLineIndex])
        {
            // Si ya se mostró la línea actual y hay más diálogo, pasa a la siguiente línea
            currentLineIndex++;
            if (currentLineIndex < dialogueLines.Length)
            {
                StartCoroutine(Typing(dialogueLines[currentLineIndex]));
            }
            else
            {
                // Termina el diálogo si no hay más líneas
                EndDialogue();
            }
        }
    }

    // Resetea el cuadro de diálogo y estado
    private void EndDialogue()
    {
        dialogueBox.SetActive(false);

        // Restaura el mundo
        UnfreezeWorld();

        dialogueText.text = "";
        currentLineIndex = 0;
        isDialogueActive = false; // Permite activar diálogos nuevamente
    }

    // Corutina que muestra el texto letra por letra
    private IEnumerator Typing(string line)
    {
        dialogueText.text = "";  // Limpia el texto previo
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.05f);  // Usa tiempo real (no afectado por TimeScale)
        }
    }

    // Detecta si el jugador entra en el rango del NPC
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Jugador en rango del NPC.");
        }
    }

    // Detecta si el jugador sale del rango del NPC
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (isDialogueActive) EndDialogue();
            Debug.Log("Jugador salió del rango del NPC.");
        }
    }

    // Congela el mundo y deshabilita el movimiento del jugador
    private void FreezeWorld()
    {
        // Congela el tiempo global
        Time.timeScale = 0;

        // Desactiva el movimiento del jugador
        if (playerState != null)
        {
            playerState.enabled = false;
        }
        else
        {
            Debug.LogWarning("PlayerState no está asignado o no encontrado.");
        }
    }

    // Restaura el mundo y habilita el movimiento del jugador
    private void UnfreezeWorld()
    {
        // Restaura el tiempo global
        Time.timeScale = 1;

        // Habilita el movimiento del jugador
        if (playerState != null)
        {
            playerState.enabled = true;
        }
    }
}
