using UnityEngine;

[System.Serializable]
public class KeyValue<tk, tv>
{
	[SerializeField] private tk key;
    [SerializeField] private tv value;

	public tk Key
	{
		get { return key; }
		set { key = value; }
	}


	public tv Value
	{
		get { return value; }
		set { this.value = value; }
	}

}
