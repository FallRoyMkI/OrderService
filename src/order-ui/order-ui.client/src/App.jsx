import './App.css';
import { Link } from 'react-router-dom';

function App() {
    return (
        <div>
            <div>
                <button>
                    <Link to='/'>Создать заказ</Link>
                </button>
                <button>
                    <Link to='/orders'>Список заказов</Link>
                </button>
            </div>
        </div>
    );
}

export default App;
