/*import "./Main.css";*/
import { Link } from 'react-router-dom';

function IndexCompany() {
    return (
        <div className="background-color wrapper">
            <Link to="/add-employee" >ADD EMPLOYEE</Link>
            <Link to="/add-equipment" >ADD EQUIPMENT</Link>
            <Link to="/add-role" >ADD ROLE</Link>
            <Link to="/add-comission" >ADD COMISSION</Link>
            <Link to="/add-warehouse" >ADD WAREHOUSE</Link>
        </div>
    );
}

export default IndexCompany;