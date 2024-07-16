using UnityEngine;
using UnityEngine.AddressableAssets;

public class GroundPropsGenerator : IGround
{
    private readonly string[] _prefabsNames = new string[]
   {
        "FluffyHurt",
        "Stone",
        "GoldApple"
   };

    private readonly int IndexGroundPropGenerator = 5;

    public void Execute(Transform transform)
    {
        int randomGeneratorIndex = Random.Range(0, 10);

        if (randomGeneratorIndex == IndexGroundPropGenerator)
        {
            string randomIndexHeals = _prefabsNames[Random.Range(0, _prefabsNames.Length)];
            Addressables.InstantiateAsync(randomIndexHeals, transform);
        }
    }
}