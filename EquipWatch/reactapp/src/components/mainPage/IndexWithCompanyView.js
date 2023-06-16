import "./Index.css";
import { Link } from 'react-router-dom';

function IndexCompanyView() {
    return (
        <div className="background-color wrapper">
            <Link to="/" >ADD EMPLOYEE</Link>
            <Link to="/" >ADD EQUIPMENT</Link>
            <Link to="/" >ADD ROLE</Link>
            <Link to="/" >ADD COMISSION</Link>
            <Link to="/" >ADD WAREHOUSE</Link>
        </div>
    );
}

export default IndexCompanyView;