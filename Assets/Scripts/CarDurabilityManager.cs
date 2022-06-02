using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDurabilityManager : MonoBehaviour 
{
    public GameObject playerCarPrefab;
    public GameObject playerCarSpawnPlace;
    public TextMesh durabilityText;
    public int NrOfLifes;

    private GameObject _playerCar;

    private void Start()
    {
        _playerCar = (GameObject)Instantiate(playerCarPrefab, playerCarSpawnPlace.transform.position, Quaternion.identity); //Quaternion.identity - перемещение префаба по умолчанию.
    }

    private void Update()
    {
        if (_playerCar.GetComponent<CarControll>().durability <= 0)
        {
            Destroy(_playerCar);
            NrOfLifes--;

            if (NrOfLifes > 0)
                StartCoroutine("SpawnCar"); //Корутины запускаются независимо от нового треда, должен быть IEnumerator.
        }
        else if (_playerCar.GetComponent<CarControll>().durability > _playerCar.GetComponent<CarControll>().maxDurability)
            _playerCar.GetComponent<CarControll>().durability = _playerCar.GetComponent<CarControll>().maxDurability;
        //если текущая прочность выше, чем максимальная, то снова устанавливается прочность 100.

        durabilityText.text = "Durability: " + _playerCar.GetComponent<CarControll>().durability + " / " + _playerCar.GetComponent<CarControll>().maxDurability;
    }

    IEnumerator SpawnCar() 
    {
        _playerCar = (GameObject)Instantiate(playerCarPrefab, playerCarSpawnPlace.transform.position, Quaternion.identity);
        _playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
        _playerCar.GetComponent<BoxCollider2D>().isTrigger = true;
        _playerCar.tag = "Untouchable"; //здесь может быть другой тег, например "пусто"

        yield return new WaitForSeconds(4); //Корутины существуют всего 3 секунды, так что какое-то время автомобиль абсолютно неуязвим. (проблемы юнити :) )
        _playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        _playerCar.GetComponent<BoxCollider2D>().isTrigger = false;
        _playerCar.tag = "Player";
    }
}
