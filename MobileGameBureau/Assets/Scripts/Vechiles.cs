using System.Collections;
using UnityEngine;

public class Vehicles : MonoBehaviour
{
    [SerializeField] private int passengerLimit;
    [SerializeField] private int passengerWeightLimit;
    [SerializeField] private int speed;
    [SerializeField] private Animator animator;

    public PassengerPickUp pickUpPoint;

    private int currentPassengerCount;
    private int currentPassengerWeight;
    private bool isDeparting;

    public void PickUpCustomer()
    {
        if (pickUpPoint == null || pickUpPoint.passengerList == null)
        {
            return;
        }

        if (isDeparting)
        {
            return;
        }

        if (currentPassengerCount >= passengerLimit)
        {
            Debug.Log("Vehicle is full and now leaving");
            return;
        }

        StartCoroutine(PickUpRoutine(1.8f));
    }

    private IEnumerator PickUpRoutine(float delay)
    {
        isDeparting = true;

        if (animator != null)
        {
            animator.SetTrigger("Go");
        }

        yield return new WaitForSeconds(delay);

        if (pickUpPoint == null || pickUpPoint.passengerList == null)
        {
            isDeparting = false;
            yield break;
        }

        int spaceLeft = passengerLimit - currentPassengerCount;
        int toRemove = Mathf.Min(spaceLeft, pickUpPoint.passengerList.Count);

        for (int i = 0; i < toRemove; i++)
        {
            var passenger = pickUpPoint.passengerList[0];
            if (passenger != null && passenger.gameObject != null)
            {
                Destroy(passenger.gameObject);
            }

            pickUpPoint.passengerList.RemoveAt(0);
            currentPassengerCount++;
        }

        bool isFull = currentPassengerCount >= passengerLimit;

        if (isFull)
        {
            Debug.Log("Vehicle is full and now leaving");
            yield return new WaitForSeconds(delay);
            if (animator != null)
            {
                animator.SetTrigger("Leave");
            }
        }
        else
        {
            isDeparting = false;
        }
    }
}