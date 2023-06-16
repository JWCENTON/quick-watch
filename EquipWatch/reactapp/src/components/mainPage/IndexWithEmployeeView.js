import "./Index.css";
import { Link } from 'react-router-dom';

function IndexEmployeeView() {
    return (
        <div className="background-color wrapper">
            <h3>EQUIPMENT :</h3>
            <Link to="/" >CHECK OUT</Link>
            <Link to="/" >CHECK IN</Link>
            <Link to="/" >RESERVE</Link>
            <Link to="/" >CHANGE COMISSION</Link>            
        </div>
    );
}

export default IndexEmployeeView;