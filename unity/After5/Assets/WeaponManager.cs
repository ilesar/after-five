using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour
{

    [SerializeField]
    private string weaponLayerName = "Weapon";

    [SerializeField]
    private Transform weraponHolder;

    [SerializeField]
    private PlayerWeapon primaryWeapon;

    private PlayerWeapon currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        EquipWeapon(primaryWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EquipWeapon (PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;

        GameObject _weaponIns = (GameObject) Instantiate(_weapon.graphics, weraponHolder.position, weraponHolder.rotation);
        _weaponIns.transform.SetParent(weraponHolder);

        if (isLocalPlayer)
        {
            _weaponIns.layer = LayerMask.NameToLayer(weaponLayerName);
        }
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
