import { Link } from 'react-router-dom';

function IndexEmployee() {
    return (
        <div className="background-color wrapper">
            <h3>EQUIPMENT :</h3>
            <Link to="/check-out" >CHECK OUT</Link>
            <Link to="/check-in" >CHECK IN</Link>
            <Link to="/reserve" >RESERVE</Link>
            <Link to="/change-comission" >CHANGE COMISSION</Link>            
        </div>
    );
}

export default IndexEmployee;