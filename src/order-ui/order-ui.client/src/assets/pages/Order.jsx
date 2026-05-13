import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';

function Order() {
  const location = useLocation()
  const { id } = location.state
  const [order, setOrder] = useState(null)

  useEffect(() => {
      getOrder();
  }, []);

  const contents = order === null
        ? <p>Не удалось найти данный заказ</p>
        : <tr key={order.id}>
              <td>{order.id}</td>
              <td>{order.cityFrom}</td>
              <td>{order.adressFrom}</td>
              <td>{order.cityTo}</td>
              <td>{order.adressTo}</td>
              <td>{order.weight}</td>
              <td>{order.pickupDate}</td>
          </tr>
    return (
        <div>
            <p>Информация о заказе</p>
            <div>
                {contents}
            </div>
        </div>
    );

      async function getOrder() {
        const response = await fetch(`/order/order/${id}`)
        if (response.ok) {
            const data = await response.json();
            setOrder(data);
        }
    }
}



export default Order;
