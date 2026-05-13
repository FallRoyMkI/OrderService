import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

function Orders() {
    const [orders, setOrders] = useState([]);
    
    useEffect(() => {
        createOrder();
    }, []);

    const contents = orders.length === 0
        ? <p>Ещё не было создано никаких заказов</p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Номер заказа</th>
                        <th>Город отправителя</th>
                        <th>Адрес отправителя</th>
                        <th>Город получателя</th>
                        <th>Адрес получателя</th>
                        <th>Вес груза</th>
                        <th>Дата забора груза</th>
                    </tr>
                </thead>
                <tbody>
                    {orders.map(order =>
                        <tr key={order.id}>
                            <td> <Link to='/order' state={{ id: order.id }}>{order.id}</Link></td>
                            <td>{order.cityFrom}</td>
                            <td>{order.adressFrom}</td>
                            <td>{order.cityTo}</td>
                            <td>{order.adressTo}</td>
                            <td>{order.weight}</td>
                            <td>{order.pickupDate}</td>
                        </tr>
                    )}
                </tbody>
            </table>;
  return (
    <div>
        <p>Список созданных заказов</p>
        <div>
            {contents}
        </div>
    </div>
  );

    async function createOrder() {
        const response = await fetch('/order/orders')
        if (response.ok) {
            const data = await response.json();
            setOrders(data);
        }
    }
}

export default Orders;
