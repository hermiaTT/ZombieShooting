using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HealthSystem
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private int currentHealth = 0;

        //[SerializeField] private GameObject bloodParticle;

        //[SerializeField] private Renderer renderer;
        //[SerializeField] private float flashTime = 0.2f;
        public int CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = value;
                
                OnHealthChange?.Invoke((float)currentHealth / maxHealth);
            }
        }

        public UnityEvent<float> OnHealthChange;

        private void Start()
        {
            CurrentHealth = maxHealth;
        }


        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                
                Reduce(1);
            }
        }

        public void Reduce(int damage)
        {
            CurrentHealth -= damage;
            //CreateHitFeedback();
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        public void AddHealth(int HealAmount)
        {
            CurrentHealth += HealAmount;
            if(CurrentHealth >= maxHealth) 
                CurrentHealth = maxHealth;
        }

        //private void CreateHitFeedback()
        //{
        //    Instantiate(bloodParticle, transform.position, Quaternion.identity);
        //    StartCoroutine(FlashFeedback());
        //}

        //private IEnumerator FlashFeedback()
        //{
        //    renderer.material.SetInt("_Flash", 1);
        //    yield return new WaitForSeconds(flashTime);
        //    renderer.material.SetInt("_Flash", 0);
        //}

        private void Die()
        {
            Debug.Log("Died");
            CurrentHealth = maxHealth;
        }
    }
}
