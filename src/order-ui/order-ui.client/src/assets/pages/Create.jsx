import { useState } from 'react';

function Create() {
  const [cityFrom, setCityFrom] = useState('')
  const [adressFrom, setAdressFrom] = useState('')
  const [cityTo, setCityTo] = useState('')
  const [adressTo, setAdressTo] = useState('')
  const [weight, setWeight] = useState(0)
  const [pickupDate, setPickupDate] = useState(null)
  const [response, setResponse] = useState(null)

  return (
    <div>
        <p>Заполните все поля чтобы создать заказ</p>
        <div>
            <div>
                <br />
                <p>Введите город отправителя</p>
                <input style={{ width: "250px" }} value={cityFrom} onChange={e => setCityFrom(e.target.value)} />
            </div>
            <div>
                <br />
                <p>Введите адресс отправителя</p>
                <input style={{ width: "250px" }} value={adressFrom} onChange={e => setAdressFrom(e.target.value)} />
            </div>
            <div>
                <br />
                <p>Введите город получателя</p>
                <input style={{ width: "250px" }} value={cityTo} onChange={e => setCityTo(e.target.value)} />
            </div>
            <div>
                <br />
                <p>Введите адресс получателя</p>
                <input style={{ width: "250px" }} value={adressTo} onChange={e => setAdressTo(e.target.value)} />
            </div>
            <div>
                <br />
                <p>Введите вес груза</p>
                <input style={{ width: "250px" }} value={weight} onChange={e => setWeight(e.target.value)} />
            </div>
            <div>
                <br />
                <p>Введите дату забора груза</p>
                <input type="date" style={{ width: "250px" }} value={pickupDate} onChange={e => setPickupDate(e.target.value)} />
            </div>
            <br />
            <button onClick={ e => createOrder(e) }>Создать</button>
        </div>
        <div>
            {response}
        </div>
    </div>
  );

    async function createOrder() {
        const response = await fetch(`/order/create`, {
            method: "POST",
            body: JSON.stringify({
                cityFrom,
                adressFrom,
                cityTo,
                adressTo,
                weight,
                pickupDate
            }),
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        })
        if (response.ok) {
            const data = await response.json();
            setResponse("Id созданного заказа: " + data);
        }
        else {
            setResponse(`http error! status: ${response.status}, message: ${await response.text() }`);
        }
    }
}

export default Create;
