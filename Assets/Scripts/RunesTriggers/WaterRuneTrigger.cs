using UnityEngine;
using System.Collections;

/// <summary>
/// Trigger water runes
/// </summary>
public class WaterRuneTrigger : MonoBehaviour
{
	[SerializeField] GameManager _gameManager;
	[SerializeField] GameObject _waterFire;
	[SerializeField] float _waterFireTime = 2.5f;

	#region Properties
	private int _waterRuneCount = 0;
	public int WaterRuneCount
	{
		get
		{
			return this._waterRuneCount;
		}
		private set
		{
			// Cannot be inferior to 0
			if (value < 0)
			{
				value = 0;
			}
			// Set limit to the water rune limit variable
			else if (value > _gameManager.runesCount.WaterRuneLimit)
			{
				value = (int)_gameManager.runesCount.WaterRuneLimit;
			}

			this._waterRuneCount = value;
		}
	}
	#endregion

	// Enable WaterFire game object for a certain time
	// Disable it after that
	private IEnumerator WaterFireEnableTime()
	{
		_waterFire.SetActive(true);

		yield return new WaitForSeconds(this._waterFireTime);

		_waterFire.SetActive(false);
	}

	// Trigger with water runes
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Rune Water")
		{
			this.WaterRuneCount += 1;
			Destroy(other.gameObject);
			_gameManager.addOxygen.AddSeconds(30);
			StartCoroutine(WaterFireEnableTime());
		}
	}
}
