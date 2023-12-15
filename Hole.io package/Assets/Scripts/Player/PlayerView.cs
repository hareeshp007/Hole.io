
using Holeio.Interact;
using System;
using UnityEngine;

namespace Holeio.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody rb;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float horizontal;
        [SerializeField]
        private float vertical;
        [SerializeField]
        private int holeSize;
        [SerializeField]
        private int scaleFactor;
        [SerializeField]
        private Vector3 newPosition;
        [SerializeField]
        private FixedJoystick playerInput;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            PlayerService.Instance.SetPlayer(this);
        }
        
        private void Update()
        {
            handleInput();
            Movement();
        }

        private void handleInput()
        {
            horizontal = playerInput.Horizontal; 
            vertical = playerInput.Vertical;


            //horizontal = Input.GetAxis("Horizontal");
            //vertical = Input.GetAxis("Vertical");

        }

        private void Movement()
        {
            if(rb != null)
            {
                if(horizontal != 0 || vertical != 0)
                {
                    rb.velocity=new Vector3 (horizontal*speed, 0, vertical*speed);
                }
            }

        }

        public void IncreaseSize()
        {
            Vector3 currentScale = transform.localScale;
            Vector3 newScale = new Vector3(currentScale.x * scaleFactor, currentScale.y, currentScale.z * scaleFactor);
            holeSize *= scaleFactor;
            transform.localScale = newScale;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Interactable>() != null)
            {
                other.gameObject.GetComponent<Interactable>().OnInteract(holeSize, true);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponent<Interactable>() != null)
            {
                other.gameObject.GetComponent<Interactable>().OnInteract(holeSize,true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Interactable>() != null)
            {
                other.gameObject.GetComponent<Interactable>().OnInteract(holeSize,false);
            }
        }

        public void SetJoystick(FixedJoystick playerJoystick)
        {
            playerInput=playerJoystick;
        }
    }
    

}
