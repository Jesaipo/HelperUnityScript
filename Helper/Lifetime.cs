using UnityEngine;
using System.Collections;


    public class Lifetime : MonoBehaviour
    {
        public float MaxAge;

        float age;

        public void Over()
        {
            age = MaxAge;
        }

        void OnEnable()
        {
            age = 0f;
        }

        void Update()
        {
            age += Time.deltaTime;
            if (age >= MaxAge)
            {
                Destroy(gameObject);
            }
        }
    }
