using UnityEngine;
using System.Collections;

public class FireRuneTrigger : MonoBehaviour
{
	[SerializeField] RunesCount _runesCount;
	[SerializeField] GameObject _redFire;
	[SerializeField] float _redFireTime = 2.5f;
	[SerializeField] GameObject[] _doors;

	#region Properties
	private int redBookCount = 0;
	public int RedBookCount
	{
		get
		{
			return this.redBookCount;
		}
		private set
		{
			// Cannot be inferior to 0
			if (value < 0)
			{
				value = 0;
			}
			// Set limit to the red book limit variable
			else if (value > _runesCount.RedBookLimit)
			{
				value = (int)_runesCount.RedBookLimit;
			}

			this.redBookCount = value;
		}
	}
	#endregion

	// Enable RedFire game object for a certain time
	// Disable it after that
	private IEnumerator WaterFireEnableTime()
	{
		_redFire.SetActive(true);

		yield return new WaitForSeconds(this._redFireTime);

		_redFire.SetActive(false);
	}

	// Trigger with fire runes
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Rune Fire")
		{
			this.redBookCount += 1;
			Destroy(other.gameObject);
			// Add sound
			StartCoroutine(WaterFireEnableTime());
			// Add sound
			if (_runesCount.BrownBookLimit == this.RedBookCount)
			{
				foreach (GameObject go in _doors)
				{
					go.SetActive(false);
				}
			}
		}
	}
}
